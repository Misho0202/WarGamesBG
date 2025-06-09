using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DnsClient.Internal;
using WarGamesBG.DL.Cache;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.Models.Configurations;
using WarGamesBG.Models.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace WarGamesBG.DL.Repositories.MongoDb
{


    public class PublisherMongoRepository : IPublisherRepository , ICacheRepository<string, Publisher>
    {
        private readonly IMongoCollection<Publisher> _publishersCollection;
        private readonly ILogger<PublisherMongoRepository> _logger;

        public PublisherMongoRepository(ILogger<PublisherMongoRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _publishersCollection = database.GetCollection<Publisher>($"{nameof(Publisher)}s");
        }


        public async Task<IEnumerable<Publisher>> DifLoad(DateTime lastExecuted)
        {
            var result = await _publishersCollection.FindAsync(g => g.DateInserted >= lastExecuted);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Publisher>> FullLoad()
        {
            return await GetAllPublishers();
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            var result = await _publishersCollection.FindAsync(g => true);

            return await result.ToListAsync();
        }

        public async Task<Publisher> GetById(string id)
        {
            var result = await _publishersCollection.FindAsync(g => g.Id == id);

            return await result.FirstOrDefaultAsync();
        }
    }
}

