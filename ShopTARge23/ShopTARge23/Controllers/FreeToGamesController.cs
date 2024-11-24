using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.FreeToGames;

namespace ShopTARge23.Controllers
{
    public class FreeToGamesController : Controller
    {
        private readonly IFreeToGamesServices _freeToGamesServices;

        public FreeToGamesController
            (
                IFreeToGamesServices freeToGamesServices
            )
        {
            _freeToGamesServices = freeToGamesServices;
        }

        public async Task<IActionResult> Index()
        {
            var gamesList = await _freeToGamesServices.FreeToGameResult();

            var viewModelList = gamesList.Select(game => new FreeToGamesIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription,
                GameUrl = game.GameUrl,
                Genre = game.Genre,
                Platform = game.Platform,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate,
                FreeToGameProfileUrl = game.FreeToGameProfileUrl
            }).ToList();


            return View(viewModelList);
        }
    }
}
