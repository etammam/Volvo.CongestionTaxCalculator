using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.Cities.Queries
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, WrappedResult<List<GetCitiesQueryResult>>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<List<GetCitiesQueryResult>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                .Include(c => c.Ignore)
                .ToListAsync(cancellationToken: cancellationToken);
            return new WrappedResult<List<GetCitiesQueryResult>>()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = ResponseMessages.OperationComplete,
                Model = _mapper.Map<List<GetCitiesQueryResult>>(cities).ToList()
            };
        }
    }
}
