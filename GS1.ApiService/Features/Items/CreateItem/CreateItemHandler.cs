using GS1.ApiService.Services;
using GS1.Database.Context;
using GS1.Database.Entities;
using GS1.ServiceDefaults.Interfaces;
using GS1.ServiceDefaults.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GS1.ApiService.Features.Items.CreateItem;

public class CreateItemHandler(GS1DbContext _dbContext, IUserContext _userContext, TimeProvider _timeProvider) : IHandler<CreateItemRequest, CreateItemResponse>
{
    private readonly CreateItemRequestValidator _validator = new();

    public async Task<Result<CreateItemResponse>> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        // Dit gedeelte zou normaal door authenticatie middleware afgevangen moeten worden en er zou dan een 401 teruggegeven moeten worden. 
        var companyId = _userContext.GetCompanyId();

        if (!companyId.HasValue)
        {
            return Result.Forbidden<CreateItemResponse>("Unauthenticated.");
        }

        // Dit gedeelte zou normaal door een generieke validatie middleware afgevangen worden.
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return Result.ValidationFailed<CreateItemResponse>(validationResult.ToDictionary());
        }

        var company = await _dbContext.Companies.FindAsync([companyId], cancellationToken);

        // Als een organisatie niet bestaat of een GTIN probeert aan te maken voor een ander bedrijf dan krijgt deze een 403 terug.
        // Dit zal waarschijnlijk opgesplitst worden in een 'Forbidden' melding en
        // een melding waarin aangegeven wordt dat het niet mogelijk is om een GTIN met deze prefix aan te maken.
        if (company == null || !request.Gtin.StartsWith(company.Code, StringComparison.Ordinal))
        {
            return Result.Forbidden<CreateItemResponse>("Forbidden.");
        }

        var item = CreateItem(request, companyId.Value);

        try
        {
            await _dbContext.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
        {
            // Alternatief op het afvangen van de exception is om zelf te van voren te kijken of de GTIN al bestaat.
            return Result.ConflictingEntity<CreateItemResponse>("GTIN already exists.");
        }

        return Result.Success(CreateResponse(item));
    }

    private static CreateItemResponse CreateResponse(Item item)
    {
        return new CreateItemResponse
        {
            Gtin = item.Gtin,
            Name = item.Name,
            VersionId = Convert.ToBase64String(item.RowVersion)
        };
    }

    private Item CreateItem(CreateItemRequest request, int companyId)
    {
        var dateTimeNow = _timeProvider.GetUtcNow();

        return new Item
        {
            Gtin = request.Gtin,
            Name = request.Name,
            CreatedAt = dateTimeNow,
            LastModifiedAt = dateTimeNow,
            CompanyId = companyId
        };
    }
}
