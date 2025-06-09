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
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, IMapper mapper, ILogger<GamesController> logger)
        {

            _gameService = gameService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _gameService.GetGames();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(string id)
        {
            if (!string.IsNullOrEmpty(id)) return BadRequest();

            var result =
                _gameService.GetGamesById(id);

            if (result == null) return NotFound();

            return Ok(result);


        }

        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame(
            [FromBody] AddGameRequest gameRequest)
        {
            if (gameRequest == null) return BadRequest();

            var game = _mapper.Map<Game>(gameRequest);

            await _gameService.AddGame(game);

            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id)) return BadRequest($"Wrong id:{id}");
            _gameService.DeleteGame(id);
            return Ok();
        }





    }
}




