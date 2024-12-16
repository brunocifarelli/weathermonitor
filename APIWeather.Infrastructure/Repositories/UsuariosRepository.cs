using APIWeather.Domain.Aggregate.Usuarios;
using APIWeather.Domain.Data;
using APIWeather.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIWeather.Infrastructure.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly APIDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UsuariosRepository(APIDbContext context)
        {
            _context = context;
        }


        public async Task CreateUsuarioAsync(string userName, string nome, string password, CancellationToken cancellationToken)
        {
            var user = new Usuarios(nome, userName, password);

            _context.Usuarios.Add(user);
            await _context.SaveAsync(cancellationToken);
        }

        public async Task RemoveUsuarioAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await FindAsync(id, cancellationToken);

            _context.Remove(user);
            await _context.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Usuarios>> GetUSuariosAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Usuarios.ToListAsync();
            return users;
        }

        public async Task<Usuarios> FindAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.Usuarios.FindAsync(userId, cancellationToken);

            if (user == null) throw new Exception("Usuário não encontrado.");

            return user;
        }

        public async Task<Usuarios> ValidateLogin(string userName, string password, CancellationToken cancellationToken)
        {
            var user = await _context.Usuarios.Where(w => w.UserName == userName && w.Senha == password)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null) throw new Exception("Nome de usuário ou senha incorreto. Tente novamente.");

            return user;
        }

        public async Task<bool> ValidateExistsUserName(string userName, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.Where(w => w.UserName == userName).AnyAsync(cancellationToken);
        }
    }
}
