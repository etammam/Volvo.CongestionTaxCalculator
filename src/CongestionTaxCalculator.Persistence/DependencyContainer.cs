using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Persistence
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<CongestionTaxCalculatorContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), builder =>
                    {
                        builder.MigrationsHistoryTable("Migrations");
                    });
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                });
            return services;
        }
    }
}
