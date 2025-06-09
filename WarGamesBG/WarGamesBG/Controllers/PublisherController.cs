using Microsoft.AspNetCore.Mvc;
using WarGamesBG.BL.Interfaces;
using WarGamesBG.Models.DTO;
using WarGamesBG.Models.Requests;
using MapsterMapper;
using WarGamesBG.Models.Responses;
using ZstdSharp.Unsafe;

namespace WarGamesBG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {


        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;

        public PublisherController(IGameService gameService,
            ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Game>> GetAll()
        {
            try
            {
                return await _gameService.GetGames();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetAll {e.Message}-{e.StackTrace}");
            }
            return await _gameService.GetGames();
        }

        public class TestRequest
        {
            public int Id { get; set; }

            public string Title { get; set; }
        }
    }
}




