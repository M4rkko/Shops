using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nancy.Json;
using ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos;
using ShopTARge23.Core.ServiceInterface;



namespace ShopTARge23.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        public async Task<OpenLocationWeatherResultDto> OpenWeatherResult(string city)
        {
            string openApiKey = "fe4e885cc104b4114c1fa726c63b1f18";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={openApiKey}&units=metric";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenLocationWeatherResultDto openResult = new JavaScriptSerializer()
                    .Deserialize<OpenLocationWeatherResultDto>(json);

                return openResult;

            }

            //return new OpenLocationWeatherResultDto();
        }
    }
}