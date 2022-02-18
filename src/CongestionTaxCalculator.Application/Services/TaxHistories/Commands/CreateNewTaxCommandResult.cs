namespace CongestionTaxCalculator.Application.Services.TaxHistories.Commands
{
    public class CreateNewTaxCommandResult
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public int CityCode { get; set; }
        public string VehicleId { get; set; }
        public decimal TaxCost { get; set; }
        public DateTime Issued { get; set; }
        public string VehicleType { get; set; }
    }
}
