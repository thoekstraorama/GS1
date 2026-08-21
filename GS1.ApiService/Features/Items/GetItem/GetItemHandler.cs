using GS1.Database.Context;
using GS1.ServiceDefaults.Interfaces;
using GS1.ServiceDefaults.Models;

namespace GS1.ApiService.Features.Items.GetItem;

public class GetItemHandler(GS1DbContext _dbContext) : IHandler<GetItemRequest, GetItemResponse>
{
    public async Task<Result<GetItemResponse>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.FindAsync([request.Gtin], cancellationToken);

        if (item == null)
        {
            return Result.NotFound<GetItemResponse>("Item not found.");
        }

        return Result.Success(new GetItemResponse
        {
            Gtin = item.Gtin,
            Name = item.Name,
            CreatedAt = item.CreatedAt,
            LastModifiedAt = item.LastModifiedAt,
            VersionId = Convert.ToBase64String(item.RowVersion),
        });
    }
}
