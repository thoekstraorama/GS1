using GS1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GS1.Database.Context;

public class GS1DbContext(DbContextOptions<GS1DbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GS1DbContext).Assembly);
    }
}
