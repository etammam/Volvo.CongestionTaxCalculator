using MediatR;
using Serilog;

namespace CongestionTaxCalculator.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information($"Start Handling {typeof(TResponse).Name}");
            var response = await next();
            _logger.Information($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}