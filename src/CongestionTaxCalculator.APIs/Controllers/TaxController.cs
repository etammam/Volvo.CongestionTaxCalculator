using CongestionTaxCalculator.APIs.Common;
using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Services.TaxHistories.Commands;
using CongestionTaxCalculator.Application.Services.TaxHistories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.APIs.Controllers
{
    public class TaxController : BaseController
    {
        [HttpPost(ApiRouter.Taxs.Create)]
        public async Task<ActionResult<WrappedResult<CreateNewTaxCommandResult>>> CreateTax([FromRoute] Guid cityId,
            [FromBody] CreateNewTaxCommand command)
        {
            command.SetCityId(cityId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }


        [HttpGet(ApiRouter.Taxs.GetTaxHistoryByVehicleId)]
        public async Task<ActionResult<WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>>> GetTaxHistoriesByVehicleId([FromRoute] string vehicleId)
        {
            var query = new GetTaxHistoryByVehicleQuery(vehicleId);
            var result = await Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet(ApiRouter.Taxs.GetTaxHistoryByCityId)]
        public async Task<ActionResult<WrappedResult<List<GetTaxHistoryByVehicleQueryResult>>>> GetTaxHistoriesByCityId([FromRoute] Guid cityId)
        {
            var query = new GetTaxHistoryByCityQuery(cityId);
            var result = await Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet(ApiRouter.Taxs.GetTaxTrollingByVehicleId)]
        public async Task<ActionResult<GetTaxQueryResult>> GetTrolling([FromRoute] string vehicleId)
        {
            var query = new GetTaxQuery(vehicleId);
            var result = await Mediator.Send(query);
            return NewResult(result);
        }

    }
}
