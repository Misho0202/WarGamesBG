using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using WarGamesBG.BL.Services;
using WarGamesBG.DL.Interfaces;
using WarGamesBG.Models.DTO;
using WarGamesBG.BL.Interfaces;

namespace WarGamesBG.Tests
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IPublisherRepository> _publisherRepositoryMock;
        private readonly Mock<IPublisherBioGateway> _publisherBioGatewayMock;

        private List<Game> _games = new()
        {
            new Game { Id = "9abda586-ff83-447f-b7e1-05dacb360c88", Title = "Counter-Strike 2", PublisherIds = ["16b080b2-f3a0-4f4c-b664-eb75989f4480", "259ffdd2-0b1d-44d2-9782-a79f9ca0f8d3"] },
            new Game { Id = "27015e6e-7f16-4d65-a589-a31a0066e782", Title = "Warthunder", PublisherIds = ["259ffdd2-0b1d-44d2-9782-a79f9ca0f8d3", "2cfae51b-5395-446e-8679-5a5a8f1c6c3e"] },
            new Game { Id = "5a4cfa98-4fdb-4414-b5fb-d23d56af1559", Title = "World-of-Tanks", PublisherIds = ["53fa6997-8557-4c86-9927-82ba3c0843f3", "16b080b2-f3a0-4f4c-b664-eb75989f4480"] },
            new Game { Id = "2c12f687-7c73-42d3-9d91-936416ffdced", Title = "CALL-OF-DUTY", PublisherIds = ["53fa6997-8557-4c86-9927-82ba3c0843f3", "bdfb76c3-f3e1-403b-b23b-c4bffbeaa6b3"] }
        };

        private List<Publisher> _publishers = new()
        {
            new Publisher { Id = "16b080b2-f3a0-4f4c-b664-eb75989f4480", Name = "Marko" },
            new Publisher { Id = "bdfb76c3-f3e1-403b-b23b-c4bffbeaa6b3", Name = "Venci" },
            new Publisher { Id = "53fa6997-8557-4c86-9927-82ba3c0843f3", Name = "Mitko" },
            new Publisher { Id = "2cfae51b-5395-446e-8679-5a5a8f1c6c3e", Name = "Nasko" },
            new Publisher { Id = "259ffdd2-0b1d-44d2-9782-a79f9ca0f8d3", Name = "Koce" }
        };

        public GameServiceTests()
        {
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _publisherBioGatewayMock = new Mock<IPublisherBioGateway>();
        }

        [Fact]
        public async Task GetGamesById_ReturnsData()
        {
            var gameId = _games[0].Id;

            _gameRepositoryMock.Setup(x => x.GetGamesById(It.IsAny<string>()))
                .ReturnsAsync((string id) => _games.FirstOrDefault(x => x.Id == id));

            var gameService = new GameService(
                _gameRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _publisherBioGatewayMock.Object);

            var result = await gameService.GetGamesById(gameId);

            Assert.NotNull(result);
            Assert.Equal(gameId, result.Id);
        }

        [Fact]
        public async Task GetById_NotExistingId()
        {
            var gameId = Guid.NewGuid().ToString();

            _gameRepositoryMock.Setup(x => x.GetGamesById(It.IsAny<string>()))
                .ReturnsAsync((string id) => _games.FirstOrDefault(g => g.Id == id));

            var gameService = new GameService(
                _gameRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _publisherBioGatewayMock.Object);

            var result = await gameService.GetGamesById(gameId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_WrongGuidId()
        {
            var gameId = "invalid-guid";

            _gameRepositoryMock.Setup(x => x.GetGamesById(It.IsAny<string>()))
                .ReturnsAsync((string id) => _games.FirstOrDefault(g => g.Id == id));

            var gameService = new GameService(
                _gameRepositoryMock.Object,
                _publisherRepositoryMock.Object,
                _publisherBioGatewayMock.Object);

            var result = await gameService.GetGamesById(gameId);

            Assert.Null(result);
        }
    }
}
