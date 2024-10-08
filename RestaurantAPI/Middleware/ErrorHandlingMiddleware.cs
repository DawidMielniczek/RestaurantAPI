﻿
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging.Configuration;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch(ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong...");
            }
           
        }
    }
}
