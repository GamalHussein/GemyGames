
using GemyGames.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GemyGames.Services
{
    public class DiviceServices : IDiviceServices
    {
        ApplicationDbContext _context;

        public DiviceServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetDivices()
        {
            return _context.Devices
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                .AsNoTracking()
                .ToList();
                
        }
    }
}
