using System.Resources;
using APIWeather.Domain.Aggregate.CidadesFavoritas;
using APIWeather.Domain.Aggregate.Usuarios;
using APIWeather.Domain.Data;
using APIWeather.Domain.Validate;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIWeather.Infrastructure.Data
{
    public class APIDbContext : DbContext, IUnitOfWork
    {

        private const int SqlServerViolationOfUniqueIndex = 2601;
        private readonly ResourceManager _resource;
        private IDbContextTransaction _currentTransaction;

        // TO CREATE MIGRATION
        //add-migration <name of migration>

        //TO REMOVE MIGRATION
        //Remove-Migration
        public APIDbContext(DbContextOptions<APIDbContext> opt) : base(opt)
        {

        }

        public DbSet<CidadesFavoritas> CidadesFavoritas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        #region Mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CidadesFavoritas>()
                .HasOne<Usuarios>()
                .WithMany()
                .HasForeignKey(k => k.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Configura comportamento ao excluir
        }
        #endregion

        #region MÉTODOS
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await SaveChangesAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction dbContextTransaction)
        {
            if (dbContextTransaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Wrong transaction: {dbContextTransaction.TransactionId}");
            }

            try
            {
                await SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }

        }

        private async Task RollbackAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync();
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException as SqlException;
                if (innerException?.Number == SqlServerViolationOfUniqueIndex && innerException.Message.Contains("UX_"))
                {
                    var startIndexNameException = innerException.Message.IndexOf("UX_");
                    var endIndexNameException = innerException.Message.IndexOf("'", startIndexNameException, StringComparison.Ordinal);
                    var lengthTotalNameException = endIndexNameException - startIndexNameException;

                    var nameIndexException = innerException.Message.Substring(startIndexNameException, lengthTotalNameException);
                    var resource = _resource.GetString(nameIndexException);

                    if (!string.IsNullOrWhiteSpace(resource))
                    {
                        throw new GuardValidationException(resource);
                    }

                    throw;
                }

                throw;
            }
        }
#endregion
    }
}
