using GS1.ApiService.Services;
using GS1.Database.Context;
using GS1.Database.Entities;
using GS1.ServiceDefaults.Interfaces;
using GS1.ServiceDefaults.Models;
using Microsoft.EntityFrameworkCore;

namespace GS1.ApiService.Features.Items.UpdateItem;

public class UpdateItemHandler(GS1DbContext _dbContext, IUserContext _userContext, TimeProvider _timeProvider) : IHandler<UpdateItemRequest, UpdateItemResponse>
{
    private readonly UpdateItemRequestValidator _validator = new();

    public async Task<Result<UpdateItemResponse>> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {
        // Dit gedeelte zou normaal door authenticatie middleware afgevangen moeten worden en er zou dan een 401 teruggegeven moeten worden. 
        var companyId = _userContext.GetCompanyId();

        if (!companyId.HasValue)
        {
            return Result.Forbidden<UpdateItemResponse>("Unauthenticated.");
        }

        // Dit gedeelte zou normaal door een generieke validatie middleware afgevangen worden.
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return Result.ValidationFailed<UpdateItemResponse>(validationResult.ToDictionary());
        }

        var item = await _dbContext.Items.Include(i => i.Company).FirstOrDefaultAsync(i => i.Gtin == request.Gtin, cancellationToken);

        if (item == null)
        {
            return Result.NotFound<UpdateItemResponse>("Item does not exist.");
        }

        if (item.CompanyId != companyId)
        {
            return Result.Forbidden<UpdateItemResponse>("Forbidden.");
        }

        item.Name = request.Name;
        item.LastModifiedAt = _timeProvider.GetUtcNow();

        _dbContext.Entry(item)
            .Property(x => x.RowVersion)
            .OriginalValue = Convert.FromBase64String(request.VersionId);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.ConflictingEntity<UpdateItemResponse>("Item already updated.");
        }

        return Result.Success(CreateResponse(item));
    }

    private static UpdateItemResponse CreateResponse(Item item)
    {
        return new UpdateItemResponse
        {
            Gtin = item.Gtin,
            Name = item.Name,
        };
    }
}
