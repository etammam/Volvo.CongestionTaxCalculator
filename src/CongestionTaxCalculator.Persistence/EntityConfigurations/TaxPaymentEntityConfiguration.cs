using CongestionTaxCalculator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Persistence.EntityConfigurations
{
    public class TaxPaymentEntityConfiguration : IEntityTypeConfiguration<TaxPayment>
    {
        public void Configure(EntityTypeBuilder<TaxPayment> builder)
        {
            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Property(t => t.Amount)
                .IsRequired();

            builder.HasOne(t => t.City)
                .WithMany(t => t.TaxPayments)
                .HasForeignKey(t => t.CityId);
        }
    }
}
