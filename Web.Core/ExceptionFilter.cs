using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Web.Core
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter(IHostingEnvironment env, ILogger logger)
        {
            HostingEnvironment = env;
            Logger = logger;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private ILogger Logger { get; }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            HttpResponse response = context.HttpContext.Response;

            context.ExceptionHandled = true;

            Logger.LogError(context.Exception, context.Exception.Message);

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            response.ContentType = "text/plain";
            string err = $"Error: {context.Exception.Message}";
            if (!HostingEnvironment.IsProduction())
            {
                err = err + Environment.NewLine;
                err = err + context.Exception.StackTrace;
            }
            response.WriteAsync(err);
        }
    }
}
