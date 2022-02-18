using CongestionTaxCalculator.Domain.Services;

namespace CongestionTaxCalculator.Infrastructure.Global
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime CurrentDateTime() => DateTime.UtcNow;

        public DateOnly CurrentDate() => new(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

        public TimeOnly CurrentTime() => new(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second);
    }
}
