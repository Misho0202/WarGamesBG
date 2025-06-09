using WarGamesBG.BackgroundServices;
using WarGamesBG.BL.Interfaces;
using WarGamesBG.BL.Services;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.DL.Repositories.MongoDb;
using WarGamesBG.Models.Configurations;
using WarGamesBG.DL.Cache;
using WarGamesBG.Models.DTO;

namespace WarGamesBG.ServiceExtensions
{
    public static class DependencyInjection    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbConfiguration>(config.GetSection(nameof(MongoDbConfiguration)));



            return services;
        }
    }
}