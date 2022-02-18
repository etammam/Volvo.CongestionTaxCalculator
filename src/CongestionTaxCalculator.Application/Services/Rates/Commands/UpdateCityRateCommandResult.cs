namespace CongestionTaxCalculator.Application.Services.Rates.Commands
{
    public class UpdateCityRateCommandResult
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public int CityCode { get; set; }
        public List<UpdateRateCommandResult> Rates { get; set; }
    }

    public record UpdateRateCommandResult(Guid Id, TimeOnly Start, TimeOnly End, decimal Rate);
}
