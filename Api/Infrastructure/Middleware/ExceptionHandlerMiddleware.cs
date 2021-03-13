using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var problem = HandleUnknownException(exception, context);
                await context
                    .Response
                    .WriteAsync(ToJson(problem));
            }
        }

        private static string ToJson(ProblemDetails problem)
        {
            return JsonSerializer.Serialize(problem, new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }

        private ProblemDetails HandleUnknownException(Exception exception, HttpContext context)
        {
            _logger.LogError(exception, exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Instance = context.Request?.Path.Value ?? string.Empty,
                Status = StatusCodes.Status500InternalServerError,
                Type = $"https://httpstatuses.com/{StatusCodes.Status500InternalServerError}"
            };
            if (_env.IsDevelopment())
            {
                problemDetails.Detail = exception.StackTrace;
                problemDetails.Title = exception.Message;
            }
            else
            {
                problemDetails.Detail = string.Empty;
                problemDetails.Title = "An unpected server fault occurred";
            }
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            return problemDetails;
        }
    }
}
