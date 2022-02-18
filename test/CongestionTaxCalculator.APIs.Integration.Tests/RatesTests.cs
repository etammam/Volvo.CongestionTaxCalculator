using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.APIs.Integration.Tests.Setups;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Application.Services.Rates.Commands;
using CongestionTaxCalculator.Domain;
using DateOnlyTimeOnly.AspNet.Converters;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CongestionTaxCalculator.APIs.Integration.Tests
{
    public class RatesTests : TestBase
    {
        private readonly ITestOutputHelper _output;

        public RatesTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CreateCityRateTest_WhenCalled_ShouldCreateCiteRates()
        {
            //Given
            var city = new City("Stockholm", 3);
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();

            var cityId = city.Id;

            var command = new CreateCityRatesCommand()
            {
                Rates = new List<CreateCityRate>()
                {
                    new(new TimeOnly(6, 30), new TimeOnly(7, 0), 10),
                    new(new TimeOnly(7, 30), new TimeOnly(8, 0), 11),
                    new(new TimeOnly(8, 30), new TimeOnly(9, 0), 12)
                }
            };

            //When
            var request =
               await HttpClient.PostAsJsonAsync(ApiRouter.Rates.Create.Replace("{cityId}", cityId.ToString()), command, new JsonSerializerOptions()
               {
                   Converters = { new TimeOnlyJsonConverter() }
               });
            var content = JsonSerializer.Deserialize<WrappedResult<CreateCityRatesCommandResult>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    Converters = { new DateOnlyJsonConverter(), new TimeOnlyJsonConverter() }
                });

            //Then
            request.StatusCode.Should().Be(HttpStatusCode.Created);
            content.StatusCode.Should().Be(HttpStatusCode.Created);
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.Model.CityId.Should().Be(cityId);
            content.Model.CityName.Should().Be(city.Name);
            content.Model.CityCode.Should().Be(city.Code);
            content.Model.Rates.Count.Should().Be(command.Rates.Count);
        }

        [Fact]
        public async Task GetCityRatesTest_WhenCalled_ShouldReturnCiteRates()
        {
            //Given
            var city = new City("Stockholm", 3)
                .SetRates(new List<Rate>()
                {
                    new(new TimeOnly(6, 30), new TimeOnly(7, 0), 10),
                    new(new TimeOnly(7, 30), new TimeOnly(8, 0), 11),
                    new(new TimeOnly(8, 30), new TimeOnly(9, 0), 12)
                });
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();

            var cityId = city.Id;

            //When
            var request =
               await HttpClient.GetAsync(ApiRouter.Rates.Get.Replace("{cityId}", cityId.ToString()));
            var contentAsString = await request.Content.ReadAsStringAsync();
            _output.WriteLine(JToken.Parse(contentAsString).ToString());
            JToken.Parse(contentAsString).HasValues.Should().BeTrue();
            var content = JToken.Parse(contentAsString).Value<JObject>()["model"].Value<JArray>();

            //Then
            request.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Count.Should().Be(3);
        }
    }
}
