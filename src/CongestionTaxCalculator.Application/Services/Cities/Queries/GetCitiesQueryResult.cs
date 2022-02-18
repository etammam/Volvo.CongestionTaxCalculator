namespace CongestionTaxCalculator.Application.Services.Cities.Queries
{
    public class GetCitiesQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public List<string> IgnoredMonths { get; set; }
        public int IgnoredDaysBeforeHoliday { get; set; }
    }
}
