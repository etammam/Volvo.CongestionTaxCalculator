using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Services.Cities.Commands;
using CongestionTaxCalculator.Application.Services.Cities.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.APIs.Controllers
{
    public class CityController : BaseController
    {
        [HttpGet(ApiRouter.City.Get)]
        public async Task<ActionResult<WrappedResult<GetCitiesQueryResult>>> GetCities()
        {
            var result = await Mediator.Send(new GetCitiesQuery());
            return NewResult(result);
        }

        [HttpPost(ApiRouter.City.Create)]
        public async Task<ActionResult<WrappedResult<CreateNewCityCommandResult>>> CreateNewCity(CreateNewCityCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(ApiRouter.City.Update)]
        public async Task<ActionResult<WrappedResult<CreateNewCityCommandResult>>> CreateNewCity([FromRoute] Guid id, UpdateCityCommand command)
        {
            command.SetId(id);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
