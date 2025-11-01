using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Middleware;

namespace Composition.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAllMiddlewares(this IApplicationBuilder builder)
        {

            builder.UseMiddleware<LoggingMiddleware>();      
            builder.UseMiddleware<ErrorHandlingMiddleware>(); 

            return builder;
        }


        public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder, string validApiKey)
        {
            builder.Use(async (context, next) =>
            {
                var apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();
                if (apiKey != validApiKey)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Access Denied");
                    return;
                }
                await next();
            });
            return builder;
        }
    }
}
