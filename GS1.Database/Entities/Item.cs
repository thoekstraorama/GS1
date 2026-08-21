namespace GS1.Database.Entities;

public class Item
{
    public required string Gtin { get; set; }

    public required string Name { get; set; }

    public required DateTimeOffset CreatedAt { get; set; }

    public required DateTimeOffset LastModifiedAt { get; set; }

    public byte[] RowVersion { get; set; } = [];

    public required int CompanyId { get; set; }

    public virtual Company? Company { get; set; }
}
