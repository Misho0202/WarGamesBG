using WarGamesBG.Models.DTO;
using WarGamesBG.Models.Requests;


namespace WarGamesBG.BL.Interfaces
{
    public interface IGameService
    {
        Task<List<Game>> GetGames();

        Task AddGame(Game game);






        Task DeleteGame(string id);
        Task<Game?> GetGamesById(string id);
        Task AddPublisher(string gameId, Publisher publisher);
    }
}
