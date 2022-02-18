namespace CongestionTaxCalculator.Application.Services.Rates.Queries
{
    public class GetCityRatesQueryResult
    {
        public Guid Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public decimal RateValue { get; set; }
    }
}
