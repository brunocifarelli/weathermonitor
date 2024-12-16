using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWeather.Domain.Aggregate.Usuarios
{
    public interface IUsuariosRepository
    {
        Task CreateUsuarioAsync(string userName, string nome, string password, CancellationToken cancellationToken);
        Task RemoveUsuarioAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Usuarios>> GetUSuariosAsync(CancellationToken cancellationToken);
        Task<Usuarios> ValidateLogin(string userName, string password, CancellationToken cancellationToken);
        Task<bool> ValidateExistsUserName(string userName, CancellationToken cancellationToken);
    }
}
