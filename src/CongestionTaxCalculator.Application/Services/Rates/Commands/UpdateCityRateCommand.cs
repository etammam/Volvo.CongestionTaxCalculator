using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Rates.Commands
{
    public class UpdateCityRateCommand : IRequest<WrappedResult<UpdateCityRateCommandResult>>
    {
        public void SetCityId(Guid cityId) => CityId = cityId;
        public Guid GetCityId => CityId;
        private Guid CityId { get; set; }
        public List<UpdateCityRateSingleDto> Rates { get; set; }
    }

    public record UpdateCityRateSingleDto(TimeOnly Start, TimeOnly End, decimal Rate);
}
