using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Rates.Commands
{
    public class CreateCityRatesCommand : IRequest<WrappedResult<CreateCityRatesCommandResult>>
    {
        public void SetCityId(Guid cityId) => CityId = cityId;
        public Guid GetCityId => CityId;
        private Guid CityId { get; set; }
        public List<CreateCityRate> Rates { get; set; }
    }

    public record CreateCityRate(TimeOnly Start, TimeOnly End, decimal Rate);
}
