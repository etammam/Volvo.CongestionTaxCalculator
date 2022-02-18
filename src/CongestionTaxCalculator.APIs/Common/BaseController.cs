using CongestionTaxCalculator.Application.Common.Wrappers;
using CongestionTaxCalculator.Application.Common.Wrappers.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CongestionTaxCalculator.APIs.Common
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        public ObjectResult NewResult<T>(WrappedResult<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => //200
                    new OkObjectResult(response),
                HttpStatusCode.Created => //201
                    new CreatedResult(string.Empty, response),
                HttpStatusCode.BadRequest => //400
                    new BadRequestObjectResult(response),
                HttpStatusCode.Forbidden => //403
                    new UnauthorizedObjectResult(new WrappedResult<T>
                    {
                        Errors = new List<ErrorModel>
                        {
                            new()
                            {
                                ErrorCode = "Unauthorized",
                                Message = "Unauthorized",
                                Property = "Overall"
                            }
                        },
                        Message = "Unauthorized",
                        Success = false
                    }),
                HttpStatusCode.NotFound => //404
                    new NotFoundObjectResult(response),
                HttpStatusCode.Accepted => new AcceptedResult(string.Empty, response),
                _ => new BadRequestObjectResult(response)
            };
        }

    }
}
