using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class UnsupportedTimeException : Exception
    {
        public UnsupportedTimeException() : base("Unsupported Time. you may need to setup the rate value of this time")
        {

        }
    }
}
