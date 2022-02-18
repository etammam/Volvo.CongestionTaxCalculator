using Ardalis.GuardClauses;
using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Exceptions;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.TaxHistories.Commands
{
    public class CreateNewTaxCommandHandler : IRequestHandler<CreateNewTaxCommand, WrappedResult<CreateNewTaxCommandResult>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;

        public CreateNewTaxCommandHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<CreateNewTaxCommandResult>> Handle(CreateNewTaxCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.Cities
                .Include(c => c.Ignore)
                .Include(c => c.Rates)
                .FirstOrDefaultAsync(c => c.Id == request.GetCityId, cancellationToken);
            Guard.Against.Null(city, nameof(city));

            var issueTime = new TimeOnly(request.Issued.Hour, request.Issued.Minute);

            var rate = city.Rates.FirstOrDefault(r => r.Start < issueTime && r.End > issueTime);
            if (rate == null)
                throw new UnsupportedTimeException();

            if (request.TaxCost != rate.RateValue)
                throw new PaymentAmountMismatchException();

            var tax = new TaxHistory()
                .SetCity(city).SetCityId(request.GetCityId).SetVehicleId(request.VehicleId)
                .SetIssued(request.Issued).SetVehicleType(request.VehicleType).SetTaxCost(request.TaxCost);

            var entry = await _context.TaxHistories.AddAsync(tax, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return new WrappedResult<CreateNewTaxCommandResult>()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.Created,
                    Message = ResponseMessages.OperationComplete,
                    Model = _mapper.Map<CreateNewTaxCommandResult>(entry.Entity)
                };
            }

            return new WrappedResult<CreateNewTaxCommandResult>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = ResponseMessages.OperationFailed,
                Success = false,
                Model = null
            };
        }
    }
}
