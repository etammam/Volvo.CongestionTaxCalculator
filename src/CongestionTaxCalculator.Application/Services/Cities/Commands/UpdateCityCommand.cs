using CongestionTaxCalculator.Application.Common.Wrappers;
using MediatR;

namespace CongestionTaxCalculator.Application.Services.Cities.Commands
{
    public class UpdateCityCommand : IRequest<WrappedResult<UpdateCityCommandResult>>
    {
        public UpdateCityCommand(string name, int code, List<string> ignoreMonths, int ignoredDaysBeforeHoliday)
        {
            Name = name;
            Code = code;
            IgnoreMonths = ignoreMonths;
            IgnoredDaysBeforeHoliday = ignoredDaysBeforeHoliday;
        }
        private Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public List<string> IgnoreMonths { get; set; }
        public int IgnoredDaysBeforeHoliday { get; set; }

        public void SetId(Guid id) => Id = id;
        public Guid GetId() => Id;
    }
}
