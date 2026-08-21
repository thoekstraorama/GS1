using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS1.Database.Entities
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .HasMaxLength(4)
                .MaxLength{4}
                .IsRequired();

            builder.Property(c => c.Name)
                .MaxLength(255)
                .IsRequired();
        }
    }
}