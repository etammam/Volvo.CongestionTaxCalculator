using AutoMapper;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Exceptions;
using CongestionTaxCalculator.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CongestionTaxCalculator.Application.Services.Cities.Commands
{
    public class CreateNewCityCommandHandler : IRequestHandler<CreateNewCityCommand, WrappedResult<CreateNewCityCommandResult>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;
        public CreateNewCityCommandHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<CreateNewCityCommandResult>> Handle(CreateNewCityCommand request, CancellationToken cancellationToken)
        {
            var cities = _context.Cities;

            var isNameDuplicated = await cities.AnyAsync(d => d.Name.ToLower() == request.Name.ToLower(), cancellationToken: cancellationToken);
            if (isNameDuplicated)
                throw new CityNameDuplicateException();

            var isCodeDuplicated = await cities.AnyAsync(c => c.Code == request.Code, cancellationToken);
            if (isCodeDuplicated)
                throw new CityCodeDuplicationException();

            var city = new City(request.Name, request.Code);
            city.SetIgnore(new Ignore(request.IgnoreMonths, request.IgnoredDaysBeforeHoliday));
            var entry = await _context.Cities.AddAsync(city, cancellationToken);
            var result = entry.Entity;
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return new WrappedResult<CreateNewCityCommandResult>()
                {
                    Success = true,
                    Message = ResponseMessages.OperationComplete,
                    StatusCode = HttpStatusCode.Created,
                    Model = _mapper.Map<CreateNewCityCommandResult>(result)
                };
            }

            return new WrappedResult<CreateNewCityCommandResult>()
            {
                Success = false,
                Message = ResponseMessages.OperationFailed,
                StatusCode = HttpStatusCode.BadRequest,
                Model = null
            };
        }
    }
}
