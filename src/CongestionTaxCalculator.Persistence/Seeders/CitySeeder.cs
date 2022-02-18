using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Persistence.Seeders
{
    public static class CitySeeder
    {
        public static List<City> GetValues()
        {
            return new List<City>()
            {
                new(Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9"), "Gothenburg", 1)
            };
        }
    }
}
