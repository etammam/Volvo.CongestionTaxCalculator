using CongestionTaxCalculator.Application.Common.Wrappers.Models;
using CongestionTaxCalculator.Application.Common.Wrappers.Models.Extensions;
using CongestionTaxCalculator.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CongestionTaxCalculator.Application.Common.Wrappers
{
    public static class ValidationMiddleware
    {
        public static void UseValidationHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    string errorText;
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;
                    if (exception.GetType() == typeof(ValidationException))
                    {
                        var validationException = (ValidationException)exception;
                        var response = new ValidationFilterOutputResponse
                        {
                            Message = "one or more validation failures has been occurred",
                            StatusCode = HttpStatusCode.BadRequest,
                            Success = false,
                            Model = null,
                            Errors = validationException.Map()
                        };
                        errorText = JsonSerializer.Serialize(response, new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else if (exception.GetType().Assembly.FullName == Assembly.GetAssembly(typeof(DomainPointer))?.FullName
                             || exception.GetType() == typeof(ArgumentException)
                             || exception.GetType() == typeof(ArgumentNullException)
                             )
                    {
                        var response = new ValidationFilterOutputResponse
                        {
                            Message = "one or more validation failures has been occurred",
                            StatusCode = HttpStatusCode.BadRequest,
                            Success = false,
                            Model = null,
                            Errors = new List<Validations>()
                            {
                                new(){Validation = new List<ValidationProperty>()
                                {
                                    new(){Message = exception.Message}
                                }}
                            }
                        };
                        errorText = JsonSerializer.Serialize(response, new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        var response = new WrappedResult<string>
                        {
                            Message = exception.Message,
                            StatusCode = HttpStatusCode.BadRequest,
                            Success = false,
                            Model = null
                        };
                        errorText = JsonSerializer.Serialize(response, new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    }

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorText, Encoding.UTF8);
                });
            });
        }
    }
}
