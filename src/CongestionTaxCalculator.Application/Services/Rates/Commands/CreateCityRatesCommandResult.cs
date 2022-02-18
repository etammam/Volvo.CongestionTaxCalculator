namespace CongestionTaxCalculator.Application.Services.Rates.Commands
{
    public class CreateCityRatesCommandResult
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public int CityCode { get; set; }
        public List<CreatedRateResult> Rates { get; set; }
    }

    public record CreatedRateResult(Guid Id, TimeOnly Start, TimeOnly End, decimal Rate);
}
