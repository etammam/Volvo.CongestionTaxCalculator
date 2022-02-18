using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Persistence.EntityConfigurations
{
    public class IgnoreEntityConfiguration : IEntityTypeConfiguration<Ignore>
    {
        public void Configure(EntityTypeBuilder<Ignore> builder)
        {
            builder.HasOne(d => d.City)
                .WithOne(d => d.Ignore)
                .HasForeignKey<Ignore>(d => d.Id);

            builder.HasData(IgnoreSeeder.GetValues());
        }
    }
}
