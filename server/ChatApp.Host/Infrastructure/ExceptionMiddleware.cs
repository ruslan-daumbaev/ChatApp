using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChatApp.Host.Infrastructure
{
    public class ExceptionMiddleware
    {
        private const string DefaultError = "Unexpected server error";
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ??
                     throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during request processing");
                await HandleExceptionAsync(context);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code = HttpStatusCode.InternalServerError, string message = DefaultError)
        {
            var result = JsonConvert.SerializeObject(new {message, code});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            await context.Response.WriteAsync(result);
        }
    }
}