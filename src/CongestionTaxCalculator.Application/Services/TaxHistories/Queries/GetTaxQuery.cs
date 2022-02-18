using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxQuery : IRequest<WrappedResult<GetTaxQueryResult>>
    {
        public GetTaxQuery(string vehicleId)
        {
            VehicleId = vehicleId;
        }

        public string VehicleId { get; set; }
    }
}
