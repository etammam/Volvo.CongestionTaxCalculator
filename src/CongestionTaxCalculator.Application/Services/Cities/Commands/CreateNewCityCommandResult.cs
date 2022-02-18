namespace CongestionTaxCalculator.Application.Services.Cities.Commands
{
    public class CreateNewCityCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public List<string> IgnoredMonths { get; set; }
        public int IgnoredDaysBeforeHoliday { get; set; }
    }
}
