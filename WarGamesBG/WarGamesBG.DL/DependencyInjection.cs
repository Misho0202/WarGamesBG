using Microsoft.Extensions.DependencyInjection;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.DL.Repositories;
using WarGamesBG.DL.Repositories.MongoDb;
using WarGamesBG.DL.Cache;
using WarGamesBG.DL.Kafka.KafkaCache;
using WarGamesBG.DL.Kafka;
using WarGamesBG.Models.Configurations.CachePopulator;
using WarGamesBG.Models.DTO;
using Microsoft.Extensions.Configuration;
using MovieStoreB.DL.Cache;
using WarGamesBG.DL.Gateways;
using Microsoft.AspNetCore.DataProtection.KeyManagement;


namespace WarGamesBG.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddDataDependencies(
                this IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, GamesRepository>();
            services.AddSingleton<IPublisherRepository, PublisherMongoRepository>();
            services.AddSingleton<IPublisherBioGateway, PublisherBioGateway>();


            return services;
        }


        //
    }
}
