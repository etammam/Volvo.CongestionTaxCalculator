using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Persistence.EntityConfigurations
{
    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired();
            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasData(CitySeeder.GetValues());
        }
    }
}
