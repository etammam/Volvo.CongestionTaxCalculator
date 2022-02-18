using System;

namespace CongestionTaxCalculator.Domain.Services
{
    public interface IDateTimeService
    {
        DateTime CurrentDateTime();
        DateOnly CurrentDate();
        TimeOnly CurrentTime();
    }
}
