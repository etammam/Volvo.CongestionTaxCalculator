using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests
{
    public class CityTests
    {
        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void SetName_WhenCalled_ShouldSetCityName()
        {
            //Given
            const string cityName = "Gothenburg";
            var city = new City();
            //When
            city.SetName(cityName);
            //Then
            city.Name.Should().Be(cityName);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void SetName_WhenCalledWithNullOrEmptyValue_ShouldThrowArgumentException()
        {
            //Given
            const string cityName = "";
            var city = new City();
            //When
            city.Invoking(c => c.SetName(cityName))
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void SetCode_WhenCalled_ShouldSetCityCode()
        {
            //Given
            const int cityCode = 1;
            var city = new City();
            //When
            city.SetCode(cityCode);
            //Then
            city.Code.Should().Be(cityCode);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void SetCode_WhenCalledWithNegativeOrZero_ShouldThrowArgumentException()
        {
            //Given
            const int cityCode = -1;
            var city = new City();
            //When
            city.Invoking(c => c.SetCode(cityCode))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Required input code cannot be zero or negative. (Parameter 'code')");
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void SetIgnore_WhenCalled_ShouldSetCityIgnore()
        {
            //Given
            var ignore = new Ignore(new List<string>() { "March", "July" }, 1);
            var city = new City();
            //When
            city.SetIgnore(ignore);
            //Then
            city.Ignore.Should().BeEquivalentTo(ignore);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void City_WhenCalled_WithIdAndNameAndCodeAndIgnore_ShouldBeFilled()
        {
            //Given
            var ignore = new Ignore(new List<string>() { "March", "July" }, 1);
            const string cityName = "Gothenburg";
            const int cityCode = 1;
            var cityId = Guid.NewGuid();
            //When
            var city = new City(cityId, cityName, cityCode, ignore);
            //Then
            city.Id.Should().Be(cityId);
            city.Name.Should().Be(cityName);
            city.Code.Should().Be(cityCode);
            city.Ignore.Should().BeEquivalentTo(ignore);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void City_WhenCalled_WithNameAndCodeAndIgnore_ShouldBeFilled()
        {
            //Given
            var ignore = new Ignore(new List<string>() { "March", "July" }, 1);
            const string cityName = "Gothenburg";
            const int cityCode = 1;
            //When
            var city = new City(cityName, cityCode, ignore);
            //Then
            city.Name.Should().Be(cityName);
            city.Code.Should().Be(cityCode);
            city.Ignore.Should().BeEquivalentTo(ignore);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void City_WhenCalled_WithIdAndNameAndCode_ShouldBeFilled()
        {
            //Given
            const string cityName = "Gothenburg";
            const int cityCode = 1;
            var cityId = Guid.NewGuid();
            //When
            var city = new City(cityId, cityName, cityCode);
            //Then
            city.Id.Should().Be(cityId);
            city.Name.Should().Be(cityName);
            city.Code.Should().Be(cityCode);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "City")]
        public void City_WhenCalled_WithNameAndCode_ShouldBeFilled()
        {
            //Given
            const string cityName = "Gothenburg";
            const int cityCode = 1;
            //When
            var city = new City(cityName, cityCode);
            //Then
            city.Name.Should().Be(cityName);
            city.Code.Should().Be(cityCode);
        }

    }
}