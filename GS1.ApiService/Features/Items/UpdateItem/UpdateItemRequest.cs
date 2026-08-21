using System.Text.Json.Serialization;

namespace GS1.ApiService.Features.Items.UpdateItem;

public class UpdateItemRequest
{
    [JsonIgnore]
    public string Gtin { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string VersionId { get; set; } = default!;
}
