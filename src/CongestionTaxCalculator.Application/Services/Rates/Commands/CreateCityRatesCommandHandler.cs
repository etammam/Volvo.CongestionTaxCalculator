using Ardalis.GuardClauses;
using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.Rates.Commands
{
    public class CreateCityRatesCommandHandler : IRequestHandler<CreateCityRatesCommand, WrappedResult<CreateCityRatesCommandResult>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;
        public CreateCityRatesCommandHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<CreateCityRatesCommandResult>> Handle(CreateCityRatesCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.Cities
                .Include(c => c.Rates)
                .FirstOrDefaultAsync(c => c.Id == request.GetCityId, cancellationToken: cancellationToken);
            Guard.Against.Null(city);
            city.SetRates(request.Rates
                .Select(r => new Rate()
                    .SetStartTime(r.Start)
                    .SetEndTime(r.End)
                    .SetRateValue(r.Rate))
                .ToList());
            _context.Update(city);
            await _context.SaveChangesAsync(cancellationToken);
            return new WrappedResult<CreateCityRatesCommandResult>()
            {
                Success = true,
                Message = ResponseMessages.OperationComplete,
                StatusCode = HttpStatusCode.Created,
                Model = new CreateCityRatesCommandResult()
                {
                    CityId = city.Id,
                    CityCode = city.Code,
                    CityName = city.Name,
                    Rates = city.Rates.Select(r => new CreatedRateResult(r.Id, r.Start, r.End, r.RateValue)).ToList()
                }
            };
        }
    }
}
