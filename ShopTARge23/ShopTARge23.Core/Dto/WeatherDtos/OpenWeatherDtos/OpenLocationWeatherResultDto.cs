using System;
using System.Text.Json.Serialization;

namespace ShopTARge23.Core.Dto.WeatherDtos.OpenWeatherDtos
{
    public class OpenLocationWeatherResultDto
    {
        public string CityName { get; set; }

        public OpenLocationMain Main { get; set; }
        public OpenLocationWind Wind { get; set; } 
        public List<OpenLocationWeather> Weather { get; set; }

    }

    public class OpenLocationMain
    {
        public double Temp { get; set; }


        public double Feels_Like { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
    } 
    public class OpenLocationWind
    {
        public double Speed { get; set; }
    }
    public class OpenLocationWeather
    {
        public string Description { get; set; }
    }
}
