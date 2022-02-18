using CongestionTaxCalculator.Domain.Exceptions;
using Nager.Date;
using System;
using System.Globalization;
using System.Linq;

namespace CongestionTaxCalculator.Domain
{
    public class TaxHistory
    {
        public TaxHistory()
        {
            Id = Guid.NewGuid();
        }

        public TaxHistory(Guid id, string vehicleId, decimal taxCost, DateTime issued, Guid cityId, VehicleTypes vehicleType)
        {
            Id = id;
            SetVehicleId(vehicleId);
            SetTaxCost(taxCost);
            SetIssued(issued);
            SetCityId(cityId);
            SetVehicleType(vehicleType);
        }

        public TaxHistory(string vehicleId, decimal taxCost, DateTime issued, Guid cityId, VehicleTypes vehicleType)
            : this(Guid.NewGuid(), vehicleId, taxCost, issued, cityId, vehicleType)
        {
        }

        public Guid Id { get; private set; }
        public string VehicleId { get; private set; }
        public decimal TaxCost { get; private set; }
        public DateTime Issued { get; private set; }
        public Guid CityId { get; private set; }
        public City City { get; set; }
        public VehicleTypes VehicleType { get; private set; }

        public TaxHistory SetVehicleId(string vehicleId)
        {
            if (!string.IsNullOrEmpty(vehicleId))
            {
                VehicleId = vehicleId;
                return this;
            }
            throw new VehicleIdNullOrEmptyException();
        }

        public TaxHistory SetTaxCost(decimal taxCost)
        {
            if (taxCost > 60)
                throw new TaxRateCanNotBeGreaterThan60Exception();
            TaxCost = taxCost;
            return this;
        }

        public TaxHistory SetIssued(DateTime issued)
        {
            //Should Validate Here
            ValidateAgainstWeekend(issued);
            ValidateAgainstNot2013(issued);
            ValidateAgainstHolidays(issued);
            ValidateAgainstDaysBeforeHoliday(issued);
            ValidateAgainstIgnoredMonth(issued);
            Issued = issued;
            return this;
        }

        public TaxHistory SetCityId(Guid cityId)
        {
            CityId = cityId;
            return this;
        }
        public TaxHistory SetCity(City city)
        {
            City = city;
            return this;
        }

        public TaxHistory SetVehicleType(VehicleTypes vehicleType)
        {
            if (vehicleType is VehicleTypes.Car or VehicleTypes.Other)
            {
                VehicleType = vehicleType;
                return this;
            }

            throw new VehicleTypeTaxExemptException(vehicleType);
        }

        private static void ValidateAgainstWeekend(DateTime issued)
        {
            if (issued.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                throw new WeekendException();
        }

        private static void ValidateAgainstNot2013(DateTime issued)
        {
            if (issued.Year != 2013)
                throw new DateOutOfSupportedRangeException();
        }

        private static void ValidateAgainstHolidays(DateTime issued)
        {
            if (DateSystem.IsPublicHoliday(issued, CountryCode.SE))
                throw new HolidayException();
        }

        private void ValidateAgainstDaysBeforeHoliday(DateTime issued)
        {
            var allowedDays = City.Ignore.DaysBeforeHoliday;
            var startDate = issued.AddDays(-allowedDays);
            var holidays = DateSystem.GetPublicHolidays(startDate, issued, CountryCode.SE);
            if (holidays.Any())
                throw new DaysBeforeHolidayException();
        }

        private void ValidateAgainstIgnoredMonth(DateTime issued)
        {
            var ignoreMonths = City.Ignore.Months;
            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(issued.Month);
            if (ignoreMonths.Contains(monthName))
                throw new IgnoredMonthException();
        }
    }
}
