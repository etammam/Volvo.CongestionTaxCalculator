using CongestionTaxCalculator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Persistence.EntityConfigurations
{
    public class TaxHistoryEntityConfiguration : IEntityTypeConfiguration<TaxHistory>
    {
        public void Configure(EntityTypeBuilder<TaxHistory> builder)
        {
            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Property(t => t.VehicleId)
                .IsRequired();

            builder.HasOne(t => t.City)
                .WithMany(c => c.TaxHistories)
                .HasForeignKey(t => t.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.TaxCost)
                .IsRequired();
        }
    }
}
