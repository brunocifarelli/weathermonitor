using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIWeather.Domain.Aggregate.CidadesFavoritas;
using APIWeather.Domain.Data;
using APIWeather.Infrastructure.Data;
using Common.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace APIWeather.Infrastructure.Repositories
{
    public class CidadesFavoritasRepository : ICidadesFavoritasRepository
    {
        private readonly APIDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CidadesFavoritasRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task CreateCidadeFavoritaAsync(string city, Guid userId, string latitude, string longitude, string cityId, CancellationToken cancellationToken)
        {
            try
            {
                await FindCidadeFavorita(userId, cityId, cancellationToken);
                var newFavoriteCity = new CidadesFavoritas(city, userId, latitude, longitude, cityId);
                _context.Add(newFavoriteCity);
                await _context.SaveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveCidadeFavoritaAsync(Guid id, CancellationToken cancellationToken)
        {
            var city = await FindAsync(id, cancellationToken);
            if (city == null)
            {
                throw new Exception("Cidade não encontrada.");
            }

            _context.Remove(city);
            await _context.SaveAsync(cancellationToken);
        }

        public async Task<CidadesFavoritas> FindCidadeFavorita(Guid userId, string CityId, CancellationToken cancellationToken)
        {
            var city = await _context.CidadesFavoritas.Where(w => w.CityId == CityId && w.UsuarioId == userId)
               .FirstOrDefaultAsync();

            if (city != null)
            {
                throw new Exception("Cidade já está marcada como favorita.");
            }

            return city;
        }
        public async Task<IEnumerable<CidadesFavoritas>> GetCidadesFavoritasAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.CidadesFavoritas.Where(w => w.UsuarioId == userId).ToListAsync();
        }

        public async Task<CidadesFavoritas> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var @params = new object[] { id };

            var cidadeFavorita = await _context
                .CidadesFavoritas
                .FindAsync(@params, cancellationToken: cancellationToken);

            return cidadeFavorita;
        }


    }
}
