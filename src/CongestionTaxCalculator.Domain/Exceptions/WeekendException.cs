using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class WeekendException : Exception
    {
        public WeekendException()
        : base("Tax can't be created in weekend")
        {
        }
    }
}
