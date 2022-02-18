using CongestionTaxCalculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Persistence
{
    public class CongestionTaxCalculatorContext : DbContext
    {
        public CongestionTaxCalculatorContext(DbContextOptions<CongestionTaxCalculatorContext> options) :
            base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Ignore> Ignores { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<TaxPayment> TaxPayments { get; set; }
        public DbSet<TaxHistory> TaxHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CongestionTaxCalculatorContext).Assembly);
        }
    }
}