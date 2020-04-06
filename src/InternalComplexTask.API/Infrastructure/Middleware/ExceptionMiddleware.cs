using System;
using System.Threading.Tasks;
using InternalComplexTask.API.Extensions;
using InternalComplexTask.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InternalComplexTask.API.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                var errorDetails = new ErrorDetails
                {
                    RequestedMethod = httpContext.Request?.Method,
                    RequestedUrl = httpContext.Request?.Path
                };

                _logger.LogError(e, "Something went wrong! {@ErrorDetails}", errorDetails);
                await httpContext.ConfigureErrorResponseAsync();
            }
        }
    }
}
