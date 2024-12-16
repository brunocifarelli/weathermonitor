using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWeather.Domain.Aggregate.CidadesFavoritas
{
    public interface ICidadesFavoritasRepository
    {
        Task CreateCidadeFavoritaAsync(string city, Guid userId, string latitude, string longitude, string cityId, CancellationToken cancellationToken);
        Task RemoveCidadeFavoritaAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<CidadesFavoritas>> GetCidadesFavoritasAsync(Guid userId, CancellationToken cancellationToken);
    }
}
