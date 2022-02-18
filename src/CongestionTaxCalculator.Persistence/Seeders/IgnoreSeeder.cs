using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Persistence.Seeders
{
    public static class IgnoreSeeder
    {
        public static List<Ignore> GetValues()
        {
            return new List<Ignore>()
            {
                new Ignore(Guid.Parse("9d315725-c8a0-45e1-9a55-fb480a477ab9"), new List<string>() {"July"}, 1)
            };
        }
    }
}
