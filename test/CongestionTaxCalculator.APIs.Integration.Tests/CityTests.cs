using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.APIs.Integration.Tests.Setups;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Application.Services.Cities.Commands;
using CongestionTaxCalculator.Application.Services.Cities.Queries;
using CongestionTaxCalculator.Domain;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CongestionTaxCalculator.APIs.Integration.Tests
{
    public class CityTests : TestBase
    {
        [Fact]
        public async Task CreateNewCity_WhenCalled_ShouldCreateANewCity()
        {
            //Given
            var command = new CreateNewCityCommand()
            {
                Name = "Stockholm",
                Code = 2,
                IgnoredDaysBeforeHoliday = 2,
                IgnoreMonths = new List<string>()
                {
                    "March"
                }
            };
            //When
            var response = await HttpClient.PostAsJsonAsync(ApiRouter.City.Create, command);
            var content =
                JsonConvert.DeserializeObject<WrappedResult<CreateNewCityCommandResult>>(
                    await response.Content.ReadAsStringAsync());
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            content.Success.Should().BeTrue();
            content.StatusCode.Should().Be(HttpStatusCode.Created);
            content.Model.Id.Should().NotBeEmpty();
            content.Model.Code.Should().Be(command.Code);
            content.Model.Name.Should().Be(command.Name);
        }

        [Fact]
        public async Task CreateNewCity_WhenCalledWithDuplicatedCityName_ShouldBadRequestWithErrorOfCityNameDuplicated()
        {
            //Given
            var cityName = "Stockholm";

            await Context.Cities.AddAsync(new City(cityName, 3));
            await Context.SaveChangesAsync();

            var command = new CreateNewCityCommand()
            {
                Name = cityName,
                Code = 2,
                IgnoredDaysBeforeHoliday = 0,
                IgnoreMonths = new List<string>()
            };
            //When
            var response = await HttpClient.PostAsJsonAsync(ApiRouter.City.Create, command);
            var content =
                JsonConvert.DeserializeObject<ValidationFilterOutputResponse>(
                    await response.Content.ReadAsStringAsync());
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Success.Should().BeFalse();
            content.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Model.Should().BeNull();
            content.Errors.First().Validation.First().Message.Should().Be("city name already found");
        }

        [Fact]
        public async Task CreateNewCity_WhenCalledWithDuplicatedCityCode_ShouldBadRequestWithErrorOfCityCodeDuplicated()
        {
            //Given
            const string cityName = "Stockholm";
            var cityCode = 2;

            await Context.Cities.AddAsync(new City(cityName, cityCode));
            await Context.SaveChangesAsync();

            var command = new CreateNewCityCommand()
            {
                Name = "Malmo",
                Code = cityCode,
                IgnoredDaysBeforeHoliday = 0,
                IgnoreMonths = new List<string>()
            };
            //When
            var response = await HttpClient.PostAsJsonAsync(ApiRouter.City.Create, command);
            var content =
                JsonConvert.DeserializeObject<ValidationFilterOutputResponse>(
                    await response.Content.ReadAsStringAsync());
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Success.Should().BeFalse();
            content.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Model.Should().BeNull();
            content.Errors.First().Validation.First().Message.Should().Be("city code already found");
        }

        [Fact]
        public async Task GetCities_WhenCalled_ShouldReturnListOfCities()
        {
            //Given
            var city = await Context.Cities.AddAsync(new City("Stockholm", 2, new Ignore(new List<string>()
            {
                "May","March"
            }, 2)));
            await Context.SaveChangesAsync();
            var expectedResult = new GetCitiesQueryResult()
            {
                Code = city.Entity.Code,
                Name = city.Entity.Name,
                Id = city.Entity.Id,
                IgnoredDaysBeforeHoliday = city.Entity.Ignore.DaysBeforeHoliday,
                IgnoredMonths = city.Entity.Ignore.Months
            };
            //When
            var response = await HttpClient.GetAsync(ApiRouter.City.Get);
            var content =
                JsonConvert.DeserializeObject<WrappedResult<List<GetCitiesQueryResult>>>(
                    await response.Content.ReadAsStringAsync());
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Model.Should().NotBeNull();
            content.Model.Should().ContainEquivalentOf(expectedResult);
        }

        [Fact]
        public async Task UpdateCity_WhenCalled_ShouldUpdateCity()
        {
            //Given
            var city = await Context.Cities.AddAsync(new City("Stockholm", 2));
            await Context.SaveChangesAsync();

            var command = new UpdateCityCommand("Paris", 3, new List<string>() { "March" }, 1);
            var expectedResult = new UpdateCityCommandResult()
            {
                Id = city.Entity.Id,
                Code = command.Code,
                Name = command.Name,
                IgnoredDaysBeforeHoliday = command.IgnoredDaysBeforeHoliday,
                IgnoredMonths = command.IgnoreMonths
            };
            //When
            var response =
                await HttpClient.PutAsJsonAsync(ApiRouter.City.Update.Replace("{id}", city.Entity.Id.ToString()),
                    command);
            var content =
                JsonConvert.DeserializeObject<WrappedResult<UpdateCityCommandResult>>(
                    await response.Content.ReadAsStringAsync());
            //Then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.Model.Should().BeEquivalentTo(expectedResult);
        }
    }
}