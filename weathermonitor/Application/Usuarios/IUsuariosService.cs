using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.Usuarios
{
    public interface IUsuariosService
    {
        Task CreateUsuarioAsync(string userName, string nome, string password, CancellationToken cancellationToken);
        Task RemoveUsuarioAsync(Guid id, CancellationToken cancellationToken);
        Task<List<UsuariosViewModel>> GetUsuariosAsync(CancellationToken cancellationToken);
        Task<UsuariosViewModel> ValidateLogin(string userName, string password, CancellationToken cancellationToken);
        Guid GetUserId();
    }
}
