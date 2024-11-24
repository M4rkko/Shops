using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.OpenWeathers;


namespace ShopTARge23.Controllers
{
    public class OpenWeathersController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeathersController
            (
            IOpenWeatherServices openWeatherServices
            )
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "OpenWeathers", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> City(string city)
        {
            OpenLocationWeatherResultDto dto = await _openWeatherServices.OpenWeatherResult(city);

            dto.CityName = city;

            //OpenWeatherViewModel vm = new();

            //vm.CityName = city;
            //vm.temp = dto.main.temp;

            return View(dto);
        }
    }
}