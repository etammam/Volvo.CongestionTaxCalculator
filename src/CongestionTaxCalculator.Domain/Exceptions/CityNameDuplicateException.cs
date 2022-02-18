using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class CityNameDuplicateException : Exception
    {
        public CityNameDuplicateException()
        : base("city name already found")
        {

        }
    }
}
