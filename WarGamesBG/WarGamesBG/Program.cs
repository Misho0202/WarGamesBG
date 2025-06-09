using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.DL.Repositories.MongoDb;
using WarGamesBG.DL.Repositories;
using WarGamesBG.DL;
using WarGamesBG.BL.Interfaces;
using WarGamesBG.BL.Services;
using WarGamesBG.Models.Requests;
using FluentValidation;
using WarGamesBG.Validators;
using WarGamesBG.Models.Configurations;
using FluentValidation.AspNetCore;
using WarGamesBG.ServiceExtensions;
using Mapster;
using Serilog.Sinks.SystemConsole.Themes;
using WarGamesBG.HealthChecks;
using WarGamesBG.BL;
using static WarGamesBG.Controllers.PublisherController;
using WarGamesBG.HealthChecks;
using WarGamesBG.ServiceExtensions;
using WarGamesBG.Validators;
using static WarGamesBG.Controllers.PublisherController;

namespace WarGamesBG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            builder.Logging.AddSerilog(logger);

            // Add services to the container
            builder.Services
                .AddConfigurations(builder.Configuration)
                .AddDataDependencies()
                .AddBusinessDependencies();

            builder.Services.AddMapster();

            builder.Services
                .AddValidatorsFromAssemblyContaining<GetAllGamesByPublisherRequestValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssemblyContaining<TestRequest>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddHealthChecks()
                .AddCheck<CustomHealthChecks>("Sampler");

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WarGamesBG");
                });
            }

            app.MapHealthChecks("/Sample");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
