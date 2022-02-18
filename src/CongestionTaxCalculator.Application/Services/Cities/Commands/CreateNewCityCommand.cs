using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Cities.Commands
{
    public class CreateNewCityCommand : IRequest<WrappedResult<CreateNewCityCommandResult>>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public List<string> IgnoreMonths { get; set; }
        public int IgnoredDaysBeforeHoliday { get; set; }
    }
}
