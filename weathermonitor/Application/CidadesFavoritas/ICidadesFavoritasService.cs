using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.CidadesFavoritas
{
    public interface ICidadesFavoritasService
    {
        Task CreateCidadeFavoritaAsync(string city, string latitude, string longitude,
            CancellationToken cancellationToken);

        Task<IEnumerable<CidadesFavoritasViewModel>> GetCidadesFavoritasAsync(CancellationToken cancellationToken);

        Task RemoveCidadeFavoritaAsync(Guid id, CancellationToken cancellationToken);
    }
}
