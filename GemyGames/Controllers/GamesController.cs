using GemyGames.ViewModel;
using Microsoft.AspNetCore.Mvc;
namespace GemyGames.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoreServices _categoreService;
        private readonly IDiviceServices _diviceServices;
        private readonly IGamesService _gamesService;
		


		public GamesController(
			ICategoreServices categoreRepsitory,
			IDiviceServices diviceServices,
			IGamesService gamesService
			)
		{
			_categoreService = categoreRepsitory;
			_diviceServices = diviceServices;
			_gamesService = gamesService;
			
		}



		public IActionResult Index()
        {
            
            var games= _gamesService.GetGames().ToList();
            return View(games);
        }


        
        public IActionResult Details(int id)
        {
           var game=_gamesService.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }
            else
            {
                return View(game);
            }
        }
        [HttpGet]
		public IActionResult Update(int id)
        {
            var game = _gamesService.GetGameById(id);
			if (game == null)
			{
				return NotFound();
			}
			else
			{
                UpdateViewModel updateViewModel = new()
                {
                    Id = id,
                    Name = game.Name,
                    Discription = game.Discription,
                    CategoreId = game.CategorieId,
                    SelectedDevices=game.Devices.Select(x => x.DeviceId).ToList(),
                    Categories=_categoreService.GetCategories(),
                    Devices=_diviceServices.GetDivices(),
                    CurrentCover=game.Cover
                };
				return View(updateViewModel);
			}
             
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories=_categoreService.GetCategories();
                model.Devices=_diviceServices.GetDivices();
                return View(model);
            }

            var game= await _gamesService.Update(model);
            if(game == null)
                { return BadRequest(); }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameForViewModel createGameForViewModel = new CreateGameForViewModel()
            {

                Categories = _categoreService.GetCategories(),

                Devices= _diviceServices.GetDivices(),

            };


            return View(createGameForViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameForViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Devices = _diviceServices.GetDivices();
                viewModel.Categories = _diviceServices.GetDivices();
                return View(viewModel);
            }
            
            await _gamesService.Create(viewModel);
            return RedirectToAction(nameof(Index));


        }

        //[HttpDelete]
        public IActionResult Delete(int id)
        {
            var Deleted = _gamesService.Delete(id);

            if (Deleted)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();

        }


    }
}
