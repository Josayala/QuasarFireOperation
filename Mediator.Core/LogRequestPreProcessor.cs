using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Mediator.Core
{
    public class LogRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public LogRequestPreProcessor(ILogger<LogRequestPreProcessor<TRequest>> logger)
        {
            Logger = logger;
        }

        private ILogger Logger { get; }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine(GetType().Name + " invoked");

            Logger.LogInformation
            (
                "Request Handled: {RequestName} | Details: {@Request}",
                typeof(TRequest).Name,
                request
            );

            return Task.CompletedTask;
        }
    }
}