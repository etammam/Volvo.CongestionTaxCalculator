using MediatR;
using Serilog;
using System.Text.Json;

namespace CongestionTaxCalculator.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public UnhandledExceptionBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                Console.ForegroundColor = ConsoleColor.Red;
                _logger.Error(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, JsonSerializer.Serialize(request));
                Console.ForegroundColor = ConsoleColor.White;
                throw;
            }
        }
    }
}
