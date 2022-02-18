using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class IgnoredMonthException : Exception
    {
        public IgnoredMonthException() : base("Tax can't be created during ignored month")
        {

        }
    }
}
