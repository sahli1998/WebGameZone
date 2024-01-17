using GameZone.Data;
using GameZone.Models;
using GameZone.ModelView;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Logging;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly ICategoryService categoryService;
        private readonly IDeviceService deviceService;
        private readonly IGamesService _gameService;
        public GamesController( ICategoryService categoryService,IDeviceService deviceservice,IGamesService gameService)
        {
           
            this.categoryService = categoryService;
            this.deviceService = deviceservice;
            this._gameService=gameService;
        }

        public IActionResult Index()
        {
            var Games = _gameService.GetAll();
            return View(Games);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Game = _gameService.getById(id);
            if(Game is null)
            {
                return BadRequest();
            }
            var model = new GameFormEdit
            {
                Id = Game!.Id,
                CategoryId = Game!.CategoryId,
                Devices = await deviceService.GetAllDevices(),
                CategoriesList = await categoryService.GetAllCategories(),
                IconName = Game.icon,
                Name = Game.Name,
                Description = Game.Description,
                DevicesId = Game.Device.Select(p => p.DeviceId).ToList()


            };
            return View(model);
        }

        public IActionResult Détails(int id)
        {
            var Game = _gameService.getById(id);
            if(Game == null)
            {
                return NotFound();
            }
            return View(Game);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var gameform = new GameFormCreate {
                CategoriesList = await categoryService.GetAllCategories(),
                Devices = await deviceService.GetAllDevices()
            };
            return View(gameform);
        }

        public async Task<IActionResult> GamesPage(int id)
        {
            var Games = _gameService.GetGamesByPages(id);
            int NumberOfPages = _gameService.TotalGamesPage();
            GameShowMV game = new GameShowMV
            {
                GameList = Games,
                TotalePageNumber = NumberOfPages,
                CurrentPageNumber = id
            };
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameFormCreate gamemodel)
        {
            if (!ModelState.IsValid)
            {
                gamemodel.CategoriesList = await categoryService.GetAllCategories();
                gamemodel.Devices = await deviceService.GetAllDevices();
                return View(gamemodel);

            }
           await _gameService.CreateGame(gamemodel);

          
            return RedirectToAction(nameof(Index));

           
        }
        [HttpDelete]
         public async Task<IActionResult> delete(int id)
        {
            var done = await _gameService.delete(id);
            if (done)
            {
                return Ok();

            }
            return BadRequest();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameFormEdit gamemodel)
        {
            if (!ModelState.IsValid)
            {
                gamemodel.CategoriesList = await categoryService.GetAllCategories();
                gamemodel.Devices = await deviceService.GetAllDevices();
                return View(gamemodel);

            }

            var valid = await _gameService.Update(gamemodel);
            if(valid is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));


        }
    }
}
