namespace GS1.ApiService.Features.Items.GetItem;

public class GetItemResponse
{
    public required string Gtin { get; set; }

    public required string Name { get; set; }

    public required DateTimeOffset CreatedAt { get; set; }

    public required DateTimeOffset LastModifiedAt { get; set; }

    public required string VersionId { get; set; }
}
