namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxQueryResult
    {
        public string VehicleId { get; set; }
        public List<TaxHistoryGroupedResponse> Histories { get; set; }
    }

    public record TaxHistoryResponse(Guid Id, TimeOnly CaptureAt, decimal Cost, string VehicleType);

    public record TaxHistoryGroupedResponse(Guid CityId, string CityName, DateTime Issue, decimal Amount, List<TaxHistoryResponse> Tax);
}
