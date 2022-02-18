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

namespace CongestionTaxCalculator.Application.Services.Cities.Commands
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, WrappedResult<UpdateCityCommandResult>>
    {
        private readonly CongestionTaxCalculatorContext _context;
        private readonly IMapper _mapper;
        public UpdateCityCommandHandler(CongestionTaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WrappedResult<UpdateCityCommandResult>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities.ToListAsync(cancellationToken: cancellationToken);
            var city = await _context.Cities
                .Include(c => c.Ignore)
                .FirstOrDefaultAsync(c => c.Id == request.GetId(), cancellationToken: cancellationToken);
            Guard.Against.Null(city);

            if (cities.Any(c => c.Name.ToLower() == request.Name.ToLower() && c.Id != request.GetId()))
                throw new CityNameDuplicateException();

            if (cities.Any(c => c.Code == request.Code && c.Id != request.GetId()))
                throw new CityCodeDuplicationException();

            city.SetName(request.Name);
            city.SetCode(request.Code);
            if (city.Ignore is null)
            {
                city.SetIgnore(new Ignore(request.IgnoreMonths, request.IgnoredDaysBeforeHoliday));
            }
            else
            {
                city.Ignore.SetMonths(request.IgnoreMonths);
                city.Ignore.SetDaysBeforeHoliday(request.IgnoredDaysBeforeHoliday);
            }


            _context.Update(city);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return new WrappedResult<UpdateCityCommandResult>()
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = _mapper.Map<UpdateCityCommandResult>(city),
                    Message = ResponseMessages.OperationComplete
                };
            }

            return new WrappedResult<UpdateCityCommandResult>()
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Model = null,
                Message = ResponseMessages.OperationFailed
            };
        }
    }
}
