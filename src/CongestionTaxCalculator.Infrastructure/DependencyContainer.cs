using CongestionTaxCalculator.Application.Services;
using CongestionTaxCalculator.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<ITaxHistoryService, TaxHistoryService>();
            services.AddTransient<ITaxPaymentService, TaxPaymentService>();
            return services;
        }
    }
}