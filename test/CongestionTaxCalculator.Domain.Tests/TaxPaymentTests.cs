using CongestionTaxCalculator.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests
{
    public class TaxPaymentTests
    {
        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetIssue_WhenCalled_ShouldSetIssue()
        {
            //Given
            var issue = DateTime.UtcNow;
            var taxPayment = new TaxPayment();
            //When
            taxPayment.SetIssued(issue);
            //Then
            taxPayment.Issued.Should().Be(issue);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetAmount_WhenCalled_ShouldSetAmount()
        {
            //Given
            const decimal amount = 45;
            var taxPayment = new TaxPayment();
            //When
            taxPayment.SetAmount(amount);
            //Then
            taxPayment.Amount.Should().Be(amount);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetAmount_WhenCalledWithMoreThan60_ShouldThrowTaxRateNotBeGreaterThan60Exception()
        {
            //Given
            const decimal amount = 65;
            var taxPayment = new TaxPayment();
            //When Then
            taxPayment.Invoking(t => t.SetAmount(amount))
                .Should()
                .ThrowExactly<TaxRateCanNotBeGreaterThan60Exception>();
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetVehicleId_WhenCalled_ShouldSetVehicleId()
        {
            //Given
            const string vehicleId = "TDA12315";
            var taxPayment = new TaxPayment();
            //When
            taxPayment.SetVehicleId(vehicleId);
            //Then
            taxPayment.VehicleId.Should().Be(vehicleId);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetVehicleId_WhenCalledWithNullOrEmptyValue_ShouldThrowArgumentNullException()
        {
            //Given
            const string vehicleId = "";
            var taxPayment = new TaxPayment();
            //When Then
            taxPayment.Invoking(t => t.SetVehicleId(vehicleId))
                .Should()
                .ThrowExactly<VehicleIdNullOrEmptyException>();
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void SetCityId_WhenCalled_ShouldSetCityId()
        {
            //Given
            var cityId = Guid.NewGuid();
            var taxPayment = new TaxPayment();
            //When
            taxPayment.SetCityId(cityId);
            //Then
            taxPayment.CityId.Should().Be(cityId);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void TaxPaymentCtor_WhenCalledWithValidParameters_ShouldFilledInstance()
        {
            //Given
            var issue = DateTime.UtcNow;
            const string vehicleId = "TDX3456";
            var cityId = Guid.NewGuid();
            const decimal amount = 45;
            //When
            var taxPayment = new TaxPayment(issue, amount, vehicleId, cityId);
            //Then
            taxPayment.CityId.Should().Be(cityId);
            taxPayment.Amount.Should().Be(amount);
            taxPayment.Issued.Should().Be(issue);
            taxPayment.VehicleId.Should().Be(vehicleId);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "TaxPayment")]
        public void TaxPaymentCtor_WhenCalledWithValidParametersAndId_ShouldFilledInstance()
        {
            //Given
            var issue = DateTime.UtcNow;
            const string vehicleId = "TDX3456";
            var cityId = Guid.NewGuid();
            const decimal amount = 45;
            var id = Guid.NewGuid();
            //When
            var taxPayment = new TaxPayment(id, issue, amount, vehicleId, cityId);
            //Then
            taxPayment.CityId.Should().Be(cityId);
            taxPayment.Amount.Should().Be(amount);
            taxPayment.Issued.Should().Be(issue);
            taxPayment.VehicleId.Should().Be(vehicleId);
            taxPayment.Id.Should().Be(id);
        }

    }
}
