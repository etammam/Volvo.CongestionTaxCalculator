using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.Rates.Queries
{
    public class GetCityRatesQueryHandler : IRequestHandler<GetCityRatesQuery, WrappedResult<List<GetCityRatesQueryResult>>>
    {
        private readonly CongestionTaxCalculatorContext _context;

        public GetCityRatesQueryHandler(CongestionTaxCalculatorContext context)
        {
            _context = context;
        }

        public async Task<WrappedResult<List<GetCityRatesQueryResult>>> Handle(GetCityRatesQuery request, CancellationToken cancellationToken)
        {
            var cityRates = await _context.Rates.Where(c => c.CityId == request.GetCityId).ToListAsync(cancellationToken: cancellationToken);
            return new WrappedResult<List<GetCityRatesQueryResult>>()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = ResponseMessages.OperationComplete,
                Model = cityRates.Select(cr => new GetCityRatesQueryResult()
                {
                    Id = cr.Id,
                    Start = cr.Start,
                    RateValue = cr.RateValue,
                    End = cr.End
                }).ToList()
            };
        }
    }
}
