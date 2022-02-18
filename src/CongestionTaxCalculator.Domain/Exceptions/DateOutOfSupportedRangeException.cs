using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class DateOutOfSupportedRangeException : Exception
    {
        public DateOutOfSupportedRangeException()
            : base("we only support 2013, try a date between")
        {
        }
    }
}
