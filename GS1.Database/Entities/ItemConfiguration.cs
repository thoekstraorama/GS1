using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS1.Database.Entities;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Gtin);

        builder.Property(i => i.Gtin)
            .HasMaxLength(14);

        builder.Property(i => i.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.CompanyId)
            .IsRequired();

        builder.Property(i => i.RowVersion)
            .IsRowVersion();
    }
}