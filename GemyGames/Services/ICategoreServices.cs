using Microsoft.AspNetCore.Mvc.Rendering;

namespace GemyGames.Services
{
    public interface ICategoreServices
    {
        public IEnumerable<SelectListItem> GetCategories();
    }
}
