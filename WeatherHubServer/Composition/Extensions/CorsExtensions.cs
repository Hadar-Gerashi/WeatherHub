using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;


namespace Composition.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddClientCors(this IServiceCollection services, string clientUrl)
        {



            services.AddCors(options =>
            {
                options.AddPolicy("AllowClientOnly", policy =>
                {
                    policy.WithOrigins(clientUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            return services;
        }

        public static IApplicationBuilder UseClientCors(this IApplicationBuilder app)
        {
            return app.UseCors("AllowClientOnly");
        }
    }
}
