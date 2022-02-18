using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Services.Rates.Commands;
using CongestionTaxCalculator.Application.Services.Rates.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.APIs.Controllers
{
    public class RateController : BaseController
    {
        [HttpPost(ApiRouter.Rates.Create)]
        public async Task<ActionResult<WrappedResult<CreateCityRatesCommandResult>>> CreateCityRates(
            [FromRoute] Guid cityId, [FromBody] CreateCityRatesCommand command)
        {
            command.SetCityId(cityId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(ApiRouter.Rates.Update)]
        public async Task<ActionResult<WrappedResult<UpdateCityRateCommandResult>>> UpdateCityRates(
            [FromRoute] Guid cityId, [FromBody] UpdateCityRateCommand command)
        {
            command.SetCityId(cityId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet(ApiRouter.Rates.Get)]
        public async Task<ActionResult<WrappedResult<GetCityRatesQueryResult>>> GetCityRates(
            [FromRoute] Guid cityId)
        {
            var command = new GetCityRatesQuery(cityId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }


    }
}
