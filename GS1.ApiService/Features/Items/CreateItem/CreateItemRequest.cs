namespace GS1.ApiService.Features.Items.CreateItem;

public class CreateItemRequest
{
    public string Gtin { get; set; } = default!;

    public string Name { get; set; } = default!;
}
