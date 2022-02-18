using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Rates.Queries
{
    public class GetCityRatesQuery : IRequest<WrappedResult<List<GetCityRatesQueryResult>>>
    {
        private Guid CityId { get; set; }
        public Guid GetCityId => CityId;

        public GetCityRatesQuery(Guid cityId)
        {
            CityId = cityId;
        }
    }
}
