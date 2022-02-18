using CongestionTaxCalculator.Domain.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests
{
    public partial class TaxHistoryTests
    {
        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetVehicleId_WhenCalled_ShouldSetVehicleId()
        {
            //Given
            const string vehicleId = "TGD1223";
            var taxHistory = new TaxHistory();
            //When
            taxHistory.SetVehicleId(vehicleId);
            //Then
            taxHistory.VehicleId.Should().Be(vehicleId);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetTaxValue_WhenCalled_ShouldSetTaxValue()
        {
            //Given
            const decimal taxCost = 15;
            var taxHistory = new TaxHistory();
            //When
            taxHistory.SetTaxCost(taxCost);
            //Then
            taxHistory.TaxCost.Should().Be(taxCost);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetIssued_WhenCalled_ShouldSetIssued()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2013, 1, 10);
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When
            taxHistory.SetIssued(issued);
            //Then
            taxHistory.Issued.Should().Be(issued);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetCityId_WhenCalled_ShouldSetCityId()
        {
            //Given
            var cityId = Guid.NewGuid();
            var taxHistory = new TaxHistory();
            //When
            taxHistory.SetCityId(cityId);
            //Then
            taxHistory.CityId.Should().Be(cityId);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetVehicleType_WhenCalled_ShouldVehicleType()
        {
            //Given
            const VehicleTypes vehicleType = VehicleTypes.Car;
            var taxHistory = new TaxHistory();
            //When
            taxHistory.SetVehicleType(vehicleType);
            //Then
            taxHistory.VehicleType.Should().Be(vehicleType);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxHistory")]
        public void SetVehicleId_WhenCalledWithNullValue_ShouldThrowException()
        {
            //Given
            string? vehicleId = null;
            var rate = new TaxHistory();
            //When Then
            rate.Invoking(t => t.SetVehicleId(vehicleId))
                .Should()
                .ThrowExactly<VehicleIdNullOrEmptyException>()
                .WithMessage($"{nameof(vehicleId)} can't be null or empty");
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void SetVehicleId_WhenCalledWithEmptyValue_ShouldThrowException()
        {
            //Given
            string vehicleId = string.Empty;
            var rate = new TaxHistory();
            //When Then
            rate.Invoking(t => t.SetVehicleId(vehicleId))
                .Should()
                .ThrowExactly<VehicleIdNullOrEmptyException>()
                .WithMessage($"{nameof(vehicleId)} can't be null or empty");
        }


        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void SetTaxValue_WhenCalledWithValueGreaterThan60_ShouldThrowException()
        {
            //Given
            const decimal taxCost = 65;
            var taxHistory = new TaxHistory();
            //When Then
            taxHistory.Invoking(t => t.SetTaxCost(taxCost))
                .Should()
                .ThrowExactly<TaxRateCanNotBeGreaterThan60Exception>()
                .WithMessage("rate value must be less or equal than 60 SKE");
        }


        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void ValidateAgainstWeekend_WhenCalledWithAWeekendDate_ShouldThrowWeekendException()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2013, 1, 5); //sunday
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When Then
            taxHistory.City.Should().BeEquivalentTo(city);
            taxHistory.Invoking(t => t.SetIssued(issued))
                .Should()
                .ThrowExactly<WeekendException>();
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void ValidateAgainstNot2013_WhenCalledWithAUnsupportedYear_ShouldThrowDateOutOfSupportedRangeException()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2022, 6, 16); //unsupported year
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When Then
            taxHistory.City.Should().BeEquivalentTo(city);
            taxHistory.Invoking(t => t.SetIssued(issued))
                .Should()
                .ThrowExactly<DateOutOfSupportedRangeException>();
        }


        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void ValidateAgainstHolidays_WhenCalledWithAHolidayDate_ShouldThrowHolidayException()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2013, 1, 1); //new year
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When Then
            taxHistory.City.Should().BeEquivalentTo(city);
            taxHistory.Invoking(t => t.SetIssued(issued))
                .Should()
                .ThrowExactly<HolidayException>();
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void ValidateAgainstDaysBeforeHoliday_WhenCalledWithADayBeforeHolidayAndInRangeOfIgnore_ShouldThrowDaysBeforeHolidayException()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2013, 1, 4); //a day before Epiphany holiday
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When Then
            taxHistory.City.Should().BeEquivalentTo(city);
            taxHistory.Invoking(t => t.SetIssued(issued))
                .Should()
                .ThrowExactly<DaysBeforeHolidayException>();
        }


        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Validator", "TaxHistory")]
        public void ValidateAgainstIgnoredMonth_WhenCalledWithIgnoredMonth_ShouldThrowIgnoredMonthException()
        {
            //Given
            var cityId = Guid.NewGuid();
            var city = new City(cityId, "Gothenburg", 1, new Ignore(new List<string>() { "March" }, 3));

            var issued = new DateTime(2013, 3, 4); //an ignored month
            var taxHistory = new TaxHistory();
            taxHistory.SetCityId(cityId);
            taxHistory.SetCity(city);
            //When Then
            taxHistory.City.Should().BeEquivalentTo(city);
            taxHistory.Invoking(t => t.SetIssued(issued))
                .Should()
                .ThrowExactly<IgnoredMonthException>();
        }

    }
}
