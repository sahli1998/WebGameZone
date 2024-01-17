using GameZone.Models;
using GameZone.ModelView;
using GameZone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IGamesService _GamesService;

        public HomeController(IGamesService GamesService)
        {
            _GamesService = GamesService;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var Games = _GamesService.GetGamesByPages(1);
            int NumberOfPages =   _GamesService.TotalGamesPage();
            GameShowMV game = new GameShowMV
            {
                GameList = Games,
                TotalePageNumber=  NumberOfPages,
                CurrentPageNumber= 1
            };
            return View(game);
        }

        [HttpPost]
        public IActionResult shearsh_name(string name)
        {
            var games = _GamesService.ShearchByName(name);
            GameShowMV games1 = new GameShowMV
            {
                GameList = games,
                TotalePageNumber = 1,
                CurrentPageNumber = 1
            };
            return View(nameof(Index),games1);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}