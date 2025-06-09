using Microsoft.Extensions.Logging;
using WarGamesBG.BL.Interfaces;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.Models.DTO;
using System.Runtime.CompilerServices;
using WarGamesBG.Models.Requests;
using DnsClient.Internal;
using WarGamesBG.Models.Responses;

namespace WarGamesBG.BL.Services
{
    internal class GameService : IGameService
    {
        private readonly IPublisherBioGateway _publisherBioGateway;
        private readonly IGameRepository _gameRepository;
        private readonly IPublisherRepository _publisherRepository;

        public GameService( IGameRepository gameRepository, IPublisherRepository publisherRepository, IPublisherBioGateway publisherBioGateway)
            
        {
            
            _gameRepository = gameRepository;
            _publisherRepository = publisherRepository;
            _publisherBioGateway = publisherBioGateway;
        }

        public async Task<List<Game>> GetGames()
        {
            return await _gameRepository.GetGames();
        }

        public async Task AddGame(Game game)
        {
            if (game == null || game.Publishers == null) return;

            foreach (var publisher in game.Publishers)
            {
                if (!Guid.TryParse(publisher, out _)) return;
            }

            await _gameRepository.AddGame(game);
        }

    
       public async Task AddPublisher(string gameId, Publisher publisher)
        {
            if (string.IsNullOrEmpty(gameId) || publisher == null) return;

            if (!Guid.TryParse(gameId, out _)) return;

            var game = await _gameRepository.GetGamesById(gameId);

            if (game == null) return;

            if (game.Publishers == null)
            {
                game.Publishers = new List<string>();
            }

            if (publisher.Id == null || string.IsNullOrEmpty(publisher.Id) || Guid.TryParse(publisher.Id, out _) == false) return;

            var existingActor = await _publisherRepository.GetById(publisher.Id);

            if (existingActor != null) return;

            game.Publishers.Add(publisher.Id);
        }

       public async Task  DeleteGame(string id)
        {
            if (!string.IsNullOrEmpty(id)) return;

            await _gameRepository.DeleteGame(id);
        }

        public async Task<Game?> GetGamesById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var gameId))
            {
                return null;
            }
            return await _gameRepository.GetGamesById(gameId.ToString());
        }
    }

}

