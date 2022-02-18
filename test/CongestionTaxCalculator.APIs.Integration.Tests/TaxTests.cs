using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.APIs.Integration.Tests.Setups;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Constants;
using CongestionTaxCalculator.Application.Services.TaxHistories.Commands;
using CongestionTaxCalculator.Application.Services.TaxHistories.Queries;
using CongestionTaxCalculator.Domain;
using DateOnlyTimeOnly.AspNet.Converters;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CongestionTaxCalculator.APIs.Integration.Tests
{
    public class TaxTests : TestBase
    {
        [Fact]
        public async Task CreateTax_WhenCalled_ShouldCreateNewTaxHistoryLog()
        {
            //Given
            var city = new City("Stockholm", 2)
                .SetIgnore(new Ignore(new List<string>() { "July" }, 2))
                .SetRates(new List<Rate>()
                {
                    new(new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                    new(new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                    new(new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                    new(new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                    new(new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                    new(new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                    new(new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                    new(new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                    new(new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                    new(new TimeOnly(18, 30), new TimeOnly(5, 59), 0),
                });
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();

            var command = new CreateNewTaxCommand()
            {
                TaxCost = 8,
                Issued = new DateTime(2013, 1, 10, 6, 15, 0),
                VehicleId = "TRD-1235",
                VehicleType = VehicleTypes.Car
            };
            //When
            var request =
                await HttpClient.PostAsJsonAsync(ApiRouter.Taxs.Create.Replace("{cityId}", city.Id.ToString()),
                    command);
            var content =
                JsonConvert.DeserializeObject<WrappedResult<CreateNewTaxCommandResult>>(
                    await request.Content.ReadAsStringAsync());
            //Then
            request.StatusCode.Should().Be(HttpStatusCode.Created);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.StatusCode.Should().Be(HttpStatusCode.Created);
            content.Model.CityId.Should().Be(city.Id);
            content.Model.CityName.Should().Be(city.Name);
            content.Model.CityCode.Should().Be(city.Code);
            content.Model.VehicleId.Should().Be(command.VehicleId);
            content.Model.Issued.Should().Be(command.Issued);
            content.Model.TaxCost.Should().Be(command.TaxCost);
            content.Model.VehicleType.Should().Be(command.VehicleType.ToString());
            content.Model.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetTaxHistoryByVehicleId_WhenCalled_ShouldReturnListOfVehicleTax()
        {
            //Given
            var city = new City("Stockholm", 2)
                .SetIgnore(new Ignore(new List<string>() { "July" }, 2))
                .SetRates(new List<Rate>()
                {
                    new(new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                    new(new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                    new(new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                    new(new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                    new(new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                    new(new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                    new(new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                    new(new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                    new(new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                    new(new TimeOnly(18, 30), new TimeOnly(5, 59), 0),
                });
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();

            const string vehicleId = "TRR-234";
            const decimal taxCost = 8;
            var issued = new DateTime(2013, 1, 10, 6, 15, 0);
            const VehicleTypes vehicleType = VehicleTypes.Car;

            var tax = new TaxHistory()
                .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                .SetIssued(issued).SetVehicleType(vehicleType).SetTaxCost(taxCost);

            await Context.TaxHistories.AddAsync(tax);
            await Context.SaveChangesAsync();

            //When
            var request =
                await HttpClient.GetAsync(ApiRouter.Taxs.GetTaxHistoryByVehicleId.Replace("{vehicleId}", vehicleId));
            var content =
                JsonConvert.DeserializeObject<WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>>(
                    await request.Content.ReadAsStringAsync());

            //Then
            request.StatusCode.Should().Be(HttpStatusCode.OK);
            content.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.Model.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetTaxHistoryByCityId_WhenCalled_ShouldReturnListOfVehicleTax()
        {
            //Given
            var city = new City("Stockholm", 2)
                .SetIgnore(new Ignore(new List<string>() { "July" }, 2))
                .SetRates(new List<Rate>()
                {
                    new(new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                    new(new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                    new(new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                    new(new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                    new(new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                    new(new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                    new(new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                    new(new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                    new(new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                    new(new TimeOnly(18, 30), new TimeOnly(5, 59), 0),
                });
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();

            const string vehicleId = "TRR-234";
            const decimal taxCost = 8;
            var issued = new DateTime(2013, 1, 10, 6, 15, 0);
            const VehicleTypes vehicleType = VehicleTypes.Car;

            var tax = new TaxHistory()
                .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                .SetIssued(issued).SetVehicleType(vehicleType).SetTaxCost(taxCost);

            await Context.TaxHistories.AddAsync(tax);
            await Context.SaveChangesAsync();

            //When
            var request =
                await HttpClient.GetAsync(ApiRouter.Taxs.GetTaxHistoryByCityId.Replace("{cityId}", city.Id.ToString()));
            var content =
                JsonConvert.DeserializeObject<WrappedResult<List<GetTaxHistoryByCityQueryResult>>>(
                    await request.Content.ReadAsStringAsync());
            //Then
            request.StatusCode.Should().Be(HttpStatusCode.OK);
            content.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.Model.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetTax_WhenCalled_ShouldReturnVehicleTax()
        {
            var city = new City("Stockholm", 2)
                .SetIgnore(new Ignore(new List<string>() { "July" }, 2))
                .SetRates(new List<Rate>()
                {
                    new(new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                    new(new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                    new(new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                    new(new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                    new(new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                    new(new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                    new(new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                    new(new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                    new(new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                    new(new TimeOnly(18, 30), new TimeOnly(5, 59), 0),
                });
            await Context.AddAsync(city);
            await Context.SaveChangesAsync();
            const string vehicleId = "TRR-234";
            const VehicleTypes vehicleType = VehicleTypes.Car;
            var taxList = new List<TaxHistory>()
            {
                new TaxHistory()
                    .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                    .SetIssued(new DateTime(2013, 1, 10, 6, 15, 0))
                    .SetVehicleType(vehicleType)
                    .SetTaxCost(8),
                new TaxHistory()
                    .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                    .SetIssued(new DateTime(2013, 1, 10, 6, 20, 0))
                    .SetVehicleType(vehicleType)
                    .SetTaxCost(8),
                new TaxHistory()
                    .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                    .SetIssued(new DateTime(2013, 1, 11, 6, 20, 0))
                    .SetVehicleType(vehicleType)
                    .SetTaxCost(8),
                new TaxHistory()
                    .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                    .SetIssued(new DateTime(2013, 1, 14, 8, 35, 0))
                    .SetVehicleType(vehicleType)
                    .SetTaxCost(8),
                new TaxHistory()
                    .SetCity(city).SetCityId(city.Id).SetVehicleId(vehicleId)
                    .SetIssued(new DateTime(2013, 1, 14, 9, 25, 0))
                    .SetVehicleType(vehicleType)
                    .SetTaxCost(8)
            };
            await Context.TaxHistories.AddRangeAsync(taxList);
            await Context.SaveChangesAsync();


            //When
            var request =
                await HttpClient.GetAsync(ApiRouter.Taxs.GetTaxTrollingByVehicleId.Replace("{vehicleId}", vehicleId));
            //var contentAsString = ;
            var content = JsonSerializer.Deserialize<WrappedResult<GetTaxQueryResult>>(await request.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new DateOnlyJsonConverter(), new TimeOnlyJsonConverter() }
            });
            //Then
            request.StatusCode.Should().Be(HttpStatusCode.OK);
            content.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Success.Should().BeTrue();
            content.Message.Should().Be(ResponseMessages.OperationComplete);
            content.Model.VehicleId.Should().Be(vehicleId);
            content.Model.Histories.Count().Should().Be(3);
            content.Model.Histories.ForEach(d => d.CityId.Should().Be(city.Id));
            content.Model.Histories.ForEach(d => d.CityName.Should().Be(city.Name));
            content.Model.Histories.ForEach(d => d.Amount.Should().BeGreaterThan(0));
        }
    }
}
