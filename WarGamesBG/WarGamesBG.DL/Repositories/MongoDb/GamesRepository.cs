using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.Models.Configurations;
using WarGamesBG.Models.DTO;
using WarGamesBG.DL.Repositories;
using WarGamesBG.DL.Cache;

namespace WarGamesBG.DL.Repositories.MongoDb
{
    internal class GamesRepository : IGameRepository
    {
        private readonly IMongoCollection<Game> _gamesCollection;
        private readonly ILogger<GamesRepository> _logger;

        public GamesRepository(ILogger<GamesRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _gamesCollection = database.GetCollection<Game>($"{nameof(Game)}s");
        }

        public async Task AddGame(Game game)
        {
            try
            {
                game.Id = Guid.NewGuid().ToString();

                await _gamesCollection.InsertOneAsync(game);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteGame(string id)
        {
            await _gamesCollection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<List<Game>> GetGames()
        {
            var result = await _gamesCollection.FindAsync(g => true);

            return result.ToList();
        }

        public async Task<Game?> GetGamesById(string id)
        {
            var result = await _gamesCollection.FindAsync(g => g.Id == id);

            return result.FirstOrDefault();
        }
    }
}


