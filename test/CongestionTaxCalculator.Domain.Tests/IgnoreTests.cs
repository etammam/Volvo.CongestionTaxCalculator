using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CongestionTaxCalculator.Domain.Tests
{
    public class IgnoreTests
    {
        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Ignore")]
        public void SetMonths_WhenCalled_ShouldSetMonths()
        {
            //Given
            var months = new List<string>() { "March", "July" };
            var ignore = new Ignore();
            //When
            ignore.SetMonths(months);
            //Then
            ignore.Months.Should().BeEquivalentTo(months);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Ignore")]
        public void SetDaysBeforeHolidays_WhenCalled_ShouldSetDaysBeforeHolidays()
        {
            //Given
            const int daysBeforeHolidays = 1;
            var ignore = new Ignore();
            //When
            ignore.SetDaysBeforeHoliday(daysBeforeHolidays);
            //Then
            ignore.DaysBeforeHoliday.Should().Be(daysBeforeHolidays);
        }

        [Fact]
        [Trait("Category", "Domain Tests")]
        [Trait("Entity", "Ignore")]
        public void Ignore_WhenCalled_WithMonthsAndDaysBeforeHolidays_ShouldBeFilled()
        {
            //Given
            var months = new List<string>() { "March", "July" };
            const int daysBeforeHolidays = 1;
            //When
            var ignore = new Ignore(months, daysBeforeHolidays);
            //Then
            ignore.Months.Should().BeEquivalentTo(months);
            ignore.DaysBeforeHoliday.Should().Be(daysBeforeHolidays);
        }
    }
}
