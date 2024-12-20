namespace GemyGames.Services
{
    public class CategoreService : ICategoreServices
    {
        ApplicationDbContext _context;
        public CategoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .AsNoTracking()
                .ToList()
                .OrderBy(c => c.Text);
                
        }
    }
}
