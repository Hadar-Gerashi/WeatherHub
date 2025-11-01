using Composition.Extensions;
using Data;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using System;
using System.Text.Json;
using Utils.Middleware;




namespace WeatherServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var clientUrl = builder.Configuration["Client:Url"];
            var apiKey = builder.Configuration["Client:ApiKey"];


            builder.Services.AddControllers();

            builder.Services.AddClientCors(clientUrl!);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "X-API-KEY",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Description = "Enter your API Key below"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                       new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                       {
                          Reference = new Microsoft.OpenApi.Models.OpenApiReference
                          {
                               Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                               Id = "ApiKey"
                          }
                       },
                        Array.Empty<string>()
                    }
                });
            });


            builder.Services.AddWeatherServices(builder.Configuration.GetConnectionString("WeatherDb")!);


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAllMiddlewares();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseClientCors();

            app.UseApiKeyValidation(apiKey!);


            app.MapControllers();

            app.Run();
        }
    }
}
