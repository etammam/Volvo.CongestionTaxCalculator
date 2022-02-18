using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class DaysBeforeHolidayException : Exception
    {
        public DaysBeforeHolidayException()
        : base("Tax can't be created before holiday")
        {
        }
    }
}
