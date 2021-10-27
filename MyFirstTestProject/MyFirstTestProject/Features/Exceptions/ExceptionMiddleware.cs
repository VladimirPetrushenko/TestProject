using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstTestProject.Features.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try{
                await _next.Invoke(context);
            }
            catch(ArgumentException)
            {
                int x = 0;
                for (int i = 1; i < 1000; i++)
                {
                    x += i;
                }
            }
        }
    }
}
