using GemyGames.Models;
using GemyGames.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GemyGames.Services
{
    public interface IGamesService
    {

        IEnumerable<Game> GetGames();
        Game? GetGameById(int id);
        Task Create(CreateGameForViewModel model);

        Task<Game?> Update(UpdateViewModel model);

        bool Delete(int id);
    }
}
