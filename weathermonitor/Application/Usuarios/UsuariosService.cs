using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Http;
using APIWeather.Domain.Aggregate.CidadesFavoritas;
using APIWeather.Domain.Aggregate.Usuarios;
using APIWeather.WebAPP.Models;

namespace APIWeather.WebAPP.Application.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _repository; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosService(IUsuariosRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateUsuarioAsync(string userName, string nome, string password, CancellationToken cancellationToken)
        {
            var existsUser = await _repository.ValidateExistsUserName(userName, cancellationToken);
            if (existsUser) throw new Exception($"Usuário {userName} já está sendo utilizado. Tente novamente.");

            await _repository.CreateUsuarioAsync(userName, nome, password, cancellationToken);
        }

        public async Task RemoveUsuarioAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.RemoveUsuarioAsync(id, cancellationToken);
        }
        
        public async Task<List<UsuariosViewModel>> GetUsuariosAsync(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetUSuariosAsync(cancellationToken);

                return user.Select(s => new UsuariosViewModel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                }).ToList();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<UsuariosViewModel> ValidateLogin(string userName, string password, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.ValidateLogin(userName, password, cancellationToken);
                
                return new UsuariosViewModel
                {
                    Id = user.Id,
                    Nome = user.Nome,
                };
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message, ex);
            }
        }

        public Guid GetUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userId = string.IsNullOrWhiteSpace(userIdString) 
                ? Guid.Empty 
                : Guid.Parse(userIdString);

            return userId;
        }
    }
}
