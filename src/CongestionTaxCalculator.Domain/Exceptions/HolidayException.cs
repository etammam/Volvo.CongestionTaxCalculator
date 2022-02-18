using System;

namespace CongestionTaxCalculator.Domain.Exceptions
{
    public class HolidayException : Exception
    {
        public HolidayException() : base("Tax can't be created in holiday")
        {
        }
    }
}
