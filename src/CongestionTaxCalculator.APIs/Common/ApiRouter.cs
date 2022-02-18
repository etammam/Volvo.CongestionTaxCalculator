namespace CongestionTaxCalculator.APIs.Common
{
    public class ApiRouter
    {
        private const string Root = "api";
        public class City
        {
            public const string Create = CityRoot;
            public const string Get = CityRoot;
            public const string Update = $"{CityRoot}/{{id}}";
            private const string CityRoot = $"{Root}/city";
        }

        public class Rates
        {
            public const string Create = $"{RateRoot}/{{cityId}}";
            public const string Get = $"{RateRoot}/{{cityId}}";
            public const string Update = $"{RateRoot}/{{cityId}}";
            private const string RateRoot = $"{Root}/rate";
        }

        public class Taxs
        {
            public const string Create = $"{TaxRoot}/{{cityId}}";
            public const string GetTaxHistoryByVehicleId = $"{TaxRoot}/vehicle/{{vehicleId}}";
            public const string GetTaxHistoryByCityId = $"{TaxRoot}/city/{{cityId}}";
            public const string GetTaxTrollingByVehicleId = $"{TaxRoot}/{{vehicleId}}";
            private const string TaxRoot = $"{Root}/taxs";
        }
    }
}
