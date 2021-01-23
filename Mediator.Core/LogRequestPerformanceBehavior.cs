using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mediator.Core
{
    public class LogRequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public LogRequestPerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("Performance Behavior entered...");
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 0)
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning(
                    "Request Duration: {Name} took ({ElapsedMilliseconds} milliseconds).  Query Details: {@Request}",
                    name, _timer.ElapsedMilliseconds, request);
            }

            Console.WriteLine("Performance Behavior exiting...");

            return response;
        }
    }
}