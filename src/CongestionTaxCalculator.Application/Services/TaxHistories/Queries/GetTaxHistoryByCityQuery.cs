using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxHistoryByCityQuery : IRequest<WrappedResult<List<GetTaxHistoryByCityQueryResult>>>
    {
        public GetTaxHistoryByCityQuery(Guid cityId)
        {
            CityId = cityId;
        }

        public Guid CityId { get; set; }
    }
}
