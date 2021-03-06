namespace CongestionTaxCalculator.Application.Services.TaxHistories.Queries
{
    public class GetTaxHistoryByVehicleQueryResult
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public decimal TaxCost { get; set; }
        public DateTime Issue { get; set; }
        public string VehicleType { get; set; }
    }
}
