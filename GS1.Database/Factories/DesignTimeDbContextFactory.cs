using GS1.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GS1.Database.Factories;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GS1DbContext>
{
    public GS1DbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GS1DbContext>();
        optionsBuilder.UseSqlServer();

        return new GS1DbContext(optionsBuilder.Options);
    }
}
