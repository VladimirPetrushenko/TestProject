using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Configuration.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var errorId = context.TraceIdentifier;
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                HandleException(context, exception, errorId);
            }
        }

        public void HandleException(HttpContext context, Exception exception, string errorId)
        {
            switch (exception)
            {
                case ArgumentException:
                    context.Response.StatusCode = 404;
                    break;
                default:
                    context.Response.StatusCode = 401;
                    break;
            }
        }
    }
}
