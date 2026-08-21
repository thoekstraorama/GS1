namespace GS1.Database.Entities;

public class Company
{
    public int Id { get; set; } = default;

    public required string Code { get; set; }

    public required string Name { get; set; }
}
