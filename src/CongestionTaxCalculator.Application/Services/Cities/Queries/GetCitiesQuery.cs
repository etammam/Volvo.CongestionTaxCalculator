using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Cities.Queries
{
    public class GetCitiesQuery : IRequest<WrappedResult<List<GetCitiesQueryResult>>>
    {

    }
}
