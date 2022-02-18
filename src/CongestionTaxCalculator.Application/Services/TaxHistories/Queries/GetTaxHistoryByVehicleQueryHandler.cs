using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxHistoryByVehicleQueryHandler : IRequestHandler<GetTaxHistoryByVehicleQuery, WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;

        public GetTaxHistoryByVehicleQueryHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>> Handle(
            GetTaxHistoryByVehicleQuery request,
            CancellationToken cancellationToken)
        {
            var taxHistories = await _context.TaxHistories
                .Include(t => t.City)
                .Where(t => t.VehicleId == request.VehicleId).ToListAsync(cancellationToken);

            return new WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>
            {
                Success = true,
                Message = ResponseMessages.OperationComplete,
                StatusCode = HttpStatusCode.OK,
                Model = _mapper.Map<List<GetTaxHistoryByVehicleQueryResult>>(taxHistories)
            };
        }
    }
}
