using CongestionTaxCalculator.Domain.Legacy;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests.Legacy
{
    public class CongestionTaxCalculatorTests
    {
        [Fact]
        public void IsTollFreeDate_WhenCalledWithWeekend_ShouldReturnTrue()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var date = new DateTime(2013, 1, 5); //weekend day
            //When
            var result = calculator.IsTollFreeDate(date);
            //Then
            result.Should().BeTrue();
        }

        [Fact]
        public void IsTollFreeDate_WhenCalledWithDateInJuly_ShouldReturnTrue()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var date = new DateTime(2013, 7, 5); //date in july
            //When
            var result = calculator.IsTollFreeDate(date);
            //Then
            result.Should().BeTrue();
        }

        [Fact]
        public void IsTollFreeDate_WhenCalledWithOutOfRangeYear_ShouldReturnFalse()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var date = new DateTime(2014, 1, 1); //out of range year
            //When
            var result = calculator.IsTollFreeDate(date);
            //Then
            result.Should().BeFalse();
        }

        [Fact]
        public void IsTollFreeDate_WhenCalledWitHoliday_ShouldReturnFalse()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var date = new DateTime(2013, 1, 1); //new year holiday
            //When
            var result = calculator.IsTollFreeDate(date);
            //Then
            result.Should().BeTrue();
        }

        [Fact]
        public void IsTollFreeVehicle_WhenCalledWithFreeVehicle_ShouldReturnTrue()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var vehicle = new Motorcycle();
            //When
            var result = calculator.IsTollFreeVehicle(vehicle);
            //Then
            result.Should().BeTrue();
        }

        [Fact]
        public void GetTax_WhenCalledWithValidParams_ShouldReturnExpectedTaxValue()
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var vehicle = new Car();
            var date = new DateTime(2013, 1, 18, 6, 15, 0);
            //When
            var result = calculator.GetTax(vehicle, new[] { date });
            //Then
            result.Should().Be(8);
        }

        [Theory]
        [InlineData(new[] { "2013-01-14 21:00:00" }, 0)]
        [InlineData(new[] { "2013-02-07 06:23:27", "2013-02-07 15:27:00" }, 21)]
        [InlineData(new[] { "2013-02-07 07:58:27", "2013-02-07 08:27:00" }, 18)]
        public void GetTax_WhenCalled_ShouldReturnAsExpected(string[] issueDate, int expectedTaxRate)
        {
            //Given
            var calculator = new LegacyCongestionTaxCalculator();
            var vehicle = new Car();
            //When
            var result = calculator.GetTax(vehicle, issueDate.Select(DateTime.Parse).ToArray());
            //Then
            result.Should().Be(expectedTaxRate);
        }
    }
}
