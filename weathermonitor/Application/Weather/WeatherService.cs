using APIWeather.Infrastructure.Injections;
using Newtonsoft.Json;
using System.Text;
using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly string UrlGeo = "geo/1.0/direct?"; //Geocoding API
        private readonly string UrlCurrentWeather = "data/2.5/weather?"; //Current Weather API
        private readonly string UrlForecastWeather = "data/2.5/forecast?"; //Forecast Weather API
        private readonly string APIKey = "49794964f8c271b02711f05e82db930a"; //API KEY

        private readonly HttpClient _client;
        public WeatherService()
        {
            _client = new WeatherClient().CreateClient();
        }

        public async Task<List<CityViewModel>> GetCityAsync(string city)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_client.BaseAddress}{UrlGeo}q={city}&limit=10&appid={APIKey}"));
            using HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($" Falha ao chamar o endpoint para a cidade: {city}", new Exception(city));
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            var citiesList = JsonConvert.DeserializeObject<List<CityViewModel>>(responseAsString);
            
            //foreach (var cityToSearch in citiesList)
            //{
            //}

            return citiesList ?? new List<CityViewModel>();
        }

        public async Task<WeatherViewModel> GetCurrentWeatherAsync(string longitude, string latitude, string city)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_client.BaseAddress}{UrlCurrentWeather}lat={latitude}&lon={longitude}&lang=pt_br&units=metric&appid={APIKey}"));
            using HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($" Falha ao chamar o endpoint para a cidade: {city}", new Exception(city));
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            WeatherViewModel weather = JsonConvert.DeserializeObject<WeatherViewModel>(responseAsString) ?? new WeatherViewModel();
            
            weather.WheatherForecastData = await GetForecastWeatherAsync(weather.Coord.Lon, weather.Coord.Lon, city);

            return weather;
        }
        private async Task<List<WeatherForecastData>> GetForecastWeatherAsync(string longitude, string latitude, string city)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_client.BaseAddress}{UrlForecastWeather}lat={latitude}&lon={longitude}&lang=pt_br&units=metric&appid={APIKey}"));
            using HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($" Falha ao chamar o endpoint para a cidade: {city}", new Exception(city));
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            WeatherForecastViewModel weather = JsonConvert.DeserializeObject<WeatherForecastViewModel>(responseAsString) ??new WeatherForecastViewModel();
            
            var weatherList = weather.List
                .GroupBy(g => g.DtTxt.Date).Where(w => w.Key != DateTime.Today)
                .Select(s => new WeatherForecastData
                {
                    Date = s.Key.ToString("dd/MM/yyyy"),
                    TempMin = s.Min(min => min.Main?.TempMin) ?? "0",
                    TempMax = s.Max(max => max.Main?.TempMax) ?? "0",
                    Description = s.GroupBy(d => d.Weather.First().Description)
                        .OrderByDescending(descGroup => descGroup.Count())
                        .First().Key,
                    Icon = s.GroupBy(d => d.Weather.First().Icon)
                    .OrderByDescending(descGroup => descGroup.Count())
                    .First().Key
                }).ToList();

            return weatherList ?? throw new Exception($"Previsão climática não encontrada.");
        }
    }
}
