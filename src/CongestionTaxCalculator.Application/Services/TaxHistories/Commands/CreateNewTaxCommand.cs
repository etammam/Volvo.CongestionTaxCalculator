using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Domain;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Commands
{
    public class CreateNewTaxCommand : IRequest<WrappedResult<CreateNewTaxCommandResult>>
    {
        private Guid CityId { get; set; }
        public Guid GetCityId => CityId;
        public void SetCityId(Guid cityId) => CityId = cityId;
        public string VehicleId { get; set; }
        public decimal TaxCost { get; set; }
        public DateTime Issued { get; set; }
        public VehicleTypes VehicleType { get; set; }
    }
}
