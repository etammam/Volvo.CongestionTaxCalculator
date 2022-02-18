using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Persistence.EntityConfigurations
{
    public class RateEntityConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.Property(r => r.Id)
                .ValueGeneratedNever();

            builder.HasOne(r => r.City)
                .WithMany(c => c.Rates)
                .HasForeignKey(r => r.CityId)
                .IsRequired();

            builder.HasData(RateSeeder.GetValues());
        }
    }
}
