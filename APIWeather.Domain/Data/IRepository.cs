
using System;
using System.Threading;
using System.Threading.Tasks;
using APIWeather.Domain.Data;
using Common.Domain.Entities;

namespace Common.Domain.Data
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        void Update(TEntity entity);
        void Add(TEntity entity);
        Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken);
    }
}
