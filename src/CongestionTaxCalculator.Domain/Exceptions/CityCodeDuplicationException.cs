using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class CityCodeDuplicationException : Exception
    {
        public CityCodeDuplicationException() : base("city code already found")
        {

        }
    }
}
