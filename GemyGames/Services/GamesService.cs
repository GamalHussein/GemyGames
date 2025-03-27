
using GemyGames.Models;
using GemyGames.Settings;
using GemyGames.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GemyGames.Services
{
	public class GamesService : IGamesService
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        private readonly string _imagesPath;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagePath}";
        }

		public IEnumerable<Game> GetGames()
		{
			return _context.Games
                .Include(e=>e.categorie)
                .Include(e=>e.Devices)
                .ThenInclude(e=>e.Device)
                .AsNoTracking()
                .ToList();
		}

        public Game? GetGameById(int id)
        {
            var game = _context.Games
                .Include(e => e.categorie)
                .Include(e => e.Devices)
                .ThenInclude(e => e.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
                
            return (game);

        }

		public async Task Create(CreateGameForViewModel model)
        {
            var coverName = await SaveCover(model.Cover);


            Game game = new()
            {
                Name = model.Name,
                Discription = model.Discription,
                CategorieId = model.CategoreId,
                Cover = coverName,
                //Search
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(UpdateViewModel model)
        {
            var game = _context.Games
                .Include(e=>e.Devices)
                .SingleOrDefault(e=>e.Id==model.Id);
            var oldCover = game.Cover;

            var ifUpdate = model.Cover is not null;

            if (game == null)
            {
                return null;
            }
            else
            {
                game.Id = model.Id;
                game.Name = model.Name;
                game.Discription = model.Discription;
                game.CategorieId = model.CategoreId;
                game.Devices = model.SelectedDevices.Select(d=>new GameDevice {DeviceId=d }).ToList();
                
                if(ifUpdate)
                {
                    var coverName = await SaveCover(model.Cover!);
                    game.Cover = coverName;
                }
            }

            var rowAffect = _context.SaveChanges();

            if(rowAffect>0)
            {
                if(ifUpdate)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);

                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);

                File.Delete(cover);
                return null;
            }
        }

        

        public bool Delete(int id)
        {
            var game = _context.Games.Find(id);

            if (game == null)
                return false;

            _context.Games.Remove(game);

            var affectRow= _context.SaveChanges();

            if(affectRow>0)
            {
                var cover=Path.Combine(_imagesPath,game.Cover);
                File.Delete(cover);
            }
            return true;
        }

        public async Task<String> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);


            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}

