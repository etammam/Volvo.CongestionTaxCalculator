using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxHistoryByVehicleQuery : IRequest<WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>>
    {
        public GetTaxHistoryByVehicleQuery(string vehicleId)
        {
            VehicleId = vehicleId;
        }

        public string VehicleId { get; set; }
    }
}
