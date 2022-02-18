using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxHistoryByCityQueryHandler : IRequestHandler<GetTaxHistoryByCityQuery, WrappedResult<List<GetTaxHistoryByCityQueryResult>>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;

        public GetTaxHistoryByCityQueryHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<List<GetTaxHistoryByCityQueryResult>>> Handle(
            GetTaxHistoryByCityQuery request,
            CancellationToken cancellationToken)
        {
            var taxHistories = await _context.TaxHistories
                .Include(t => t.City)
                .Where(t => t.CityId == request.CityId).ToListAsync(cancellationToken);

            return new WrappedResult<List<GetTaxHistoryByCityQueryResult>>
            {
                Success = true,
                Message = ResponseMessages.OperationComplete,
                StatusCode = HttpStatusCode.OK,
                Model = _mapper.Map<List<GetTaxHistoryByCityQueryResult>>(taxHistories)
            };
        }
    }
}
