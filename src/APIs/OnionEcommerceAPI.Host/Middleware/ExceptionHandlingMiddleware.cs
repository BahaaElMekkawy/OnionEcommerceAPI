using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OnionEcommerceAPI.Core.Application.Common.Exception;
using OnionEcommerceAPI.Web.Errors;

namespace OnionEcommerceAPI.Host.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next
            , ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            ApiResponse response;
            switch (exception)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(404, exception.Message);
                    break;
                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(400, exception.Message);
                    break;
                default:
                    if (_environment.IsDevelopment())
                    {
                        _logger.LogError(exception.Message);
                        response = new ApiResponse(500, exception.Message);
                    }
                    else
                    {
                        response = new ApiResponse(500);
                    }
                    httpContext.Response.StatusCode = 500;
                    httpContext.Response.ContentType = "application/json";
                    break;
            }
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

    }
}
