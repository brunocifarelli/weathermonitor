using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.Weather
{
    public interface IWeatherService
    {
        Task<List<CityViewModel>> GetCityAsync(string city);
        Task<WeatherViewModel> GetCurrentWeatherAsync(string longitude, string latitude, string city);
    }
}
