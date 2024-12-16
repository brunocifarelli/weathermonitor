using System.Threading;
using System.Threading.Tasks;

namespace APIWeather.Domain.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);
    }
}