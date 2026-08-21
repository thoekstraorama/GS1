namespace GS1.ApiService.Features.Items.CreateItem;

public class CreateItemResponse
{
    public required string Gtin { get; set; }

    public required string Name { get; set; }

    public required string VersionId { get; set; }
}
