using CongestionTaxCalculator.Application.Contracts.Cities;
using CongestionTaxCalculator.Application.Services;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence;

namespace CongestionTaxCalculator.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly CongestionTaxCalculatorContext _context;

        public CityService(CongestionTaxCalculatorContext context)
        {
            _context = context;
        }

        public async Task<CreateNewCityCommandResult> CreateAsync(CreateNewCityCommand command)
        {
            var city = new City(command.Name, command.Code);
            var entry = await _context.Cities.AddAsync(city);

        }
    }
}
