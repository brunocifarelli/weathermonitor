using APIWeather.WebAPP.Application.Weather;
using APIWeather.WebAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace weathermonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService; 

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _logger = logger;
        }
        /// <summary>
        /// Método utilizado para buscar a longitude e a latitude da cidade para assim,
        /// realizar a busca da previsão de temperatura.
        /// </summary>
        /// <param name="city">nome da cidade para ser realizada a busca</param>
        /// <returns></returns>
        [HttpGet("city/{city}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityViewModel>>> GetCityAsync([FromRoute] string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city)) return BadRequest("Inconsistência nos dados.");
                var cities = await _weatherService.GetCityAsync(city);

                return Ok(cities);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Método utilizado para buscar o clima da cidade
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("weather")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityViewModel>>> GetCurrentWeatherAsync([FromQuery] WeatherParams city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city.Lat) || string.IsNullOrWhiteSpace(city.Lon)) return BadRequest("Inconsistência nos dados.");
                var cities = await _weatherService.GetCurrentWeatherAsync(city.Lon, city.Lat, city.Name);

                return Ok(cities);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}