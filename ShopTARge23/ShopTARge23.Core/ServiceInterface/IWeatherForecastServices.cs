using ShopTARge23.Core.Dto.WeatherDtos.AccuWeatherDtos;
namespace ShopTARge23.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
    }
}
