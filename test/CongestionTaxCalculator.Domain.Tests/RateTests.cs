using CongestionTaxCalculator.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests
{
    public class RateTests
    {
        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Rate")]
        public void SetStartTime_WhenCalled_ShouldSetStartTime()
        {
            //Given
            var startTime = new TimeOnly(9, 10);
            var rate = new Rate();
            //When
            rate.SetStartTime(startTime);
            //Then
            rate.Start.Should().Be(startTime);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Rate")]
        public void SetEndTime_WhenCalled_ShouldSetEndTime()
        {
            //Given
            var endTime = new TimeOnly(9, 10);
            var rate = new Rate();
            //When
            rate.SetEndTime(endTime);
            //Then
            rate.End.Should().Be(endTime);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Rate")]
        public void SetRateValue_WhenCalled_ShouldSetRateValue()
        {
            //Given
            const decimal rateValue = 8;
            var rate = new Rate();
            //When
            rate.SetRateValue(rateValue);
            //Then
            rate.RateValue.Should().Be(rateValue);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Rate")]
        public void SetRateValue_WhenCalledWithValueGreaterThan60_ShouldThrowException()
        {
            //Given
            const decimal rateValue = 68;
            var rate = new Rate();
            //When And Then
            rate.Invoking(r => r.SetRateValue(rateValue))
                .Should()
                .ThrowExactly<TaxRateCanNotBeGreaterThan60Exception>()
                .WithMessage("rate value must be less or equal than 60 SKE");
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Rate")]
        public void SetCityId_WhenCalled_ShouldSetCityId()
        {
            //Given
            var cityId = Guid.NewGuid();
            var rate = new Rate();
            //When
            rate.SetCityId(cityId);
            //Then
            rate.CityId.Should().Be(cityId);
        }
    }
}
