using APIWeather.Domain.Aggregate.CidadesFavoritas;
using APIWeather.WebAPP.Application.CidadesFavoritas;
using APIWeather.WebAPP.Application.Usuarios;
using APIWeather.WebAPP.Application.Weather;
using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.CidadesFavoritas
{
    public class CidadesFavoritasService : ICidadesFavoritasService
    {
        private readonly ICidadesFavoritasRepository _repository;
        private readonly IUsuariosService _usuariosService;
        private readonly IWeatherService _weatherService;
        public CidadesFavoritasService(ICidadesFavoritasRepository repository, IUsuariosService usuariosService, IWeatherService  weatherService)
        {
            _repository = repository;
            _usuariosService = usuariosService;
            _weatherService = weatherService;
        }
        //Guid id, string nome, Guid usuarioId, string latitude, string longitude
        public async Task CreateCidadeFavoritaAsync(string cityName, string latitude, string longitude, CancellationToken cancellationToken)
        {
            var userId = _usuariosService.GetUserId();
            WeatherViewModel city = await _weatherService.GetCurrentWeatherAsync(longitude, latitude, cityName);
            await _repository.CreateCidadeFavoritaAsync(cityName, userId, latitude, longitude, city.Id, cancellationToken);
        }

        public async Task<IEnumerable<CidadesFavoritasViewModel>> GetCidadesFavoritasAsync(CancellationToken cancellationToken)
        {
            var userId = _usuariosService.GetUserId();
            var cities = await _repository.GetCidadesFavoritasAsync(userId, cancellationToken);

            return cities.Select(s => new CidadesFavoritasViewModel {
                Id = s.CityId,
                UsuarioId = s.UsuarioId,
                Lat = s.Latitude,
                Lon = s.Longitude,
                Name = s.Nome,
                FavoriteCityId = s.Id
            }).ToList();
        }

        public async Task RemoveCidadeFavoritaAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.RemoveCidadeFavoritaAsync(id, cancellationToken);
        }
    }
}
