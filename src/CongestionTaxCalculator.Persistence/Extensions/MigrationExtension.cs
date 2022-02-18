using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Persistence.Extensions
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder AutomaticMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            scope?.ServiceProvider.GetRequiredService<CongestionTaxCalculatorContext>().Database.Migrate();
            return app;
        }
    }
}
