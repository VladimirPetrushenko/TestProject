using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Common.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                HandleException(context, exception);
            }
        }

        public static void HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                ArgumentException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status400BadRequest,
            };
        }
    }
}
