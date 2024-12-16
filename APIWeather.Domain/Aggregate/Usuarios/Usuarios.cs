using APIWeather.Domain.Validate;

namespace APIWeather.Domain.Aggregate.Usuarios
{
    public class Usuarios
    {
        public Usuarios(string nome, string userName, string senha)
        {
            Guard.Validate(errorIf =>
            {
                errorIf
                    .IsNullOrEmptyString(nome, nameof(nome),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado", nameof(nome)))
                    .IsNullOrEmptyString(userName, nameof(userName),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado", nameof(userName)))
                    .IsNullOrEmptyString(senha, nameof(senha),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado",
                            nameof(senha)));
            });

            Id = Guid.NewGuid();
            Nome = nome;
            UserName = userName;
            Senha = senha;
        }

        /// <summary>
        /// Chave primária do usuário
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; private set; }
        /// <summary>
        /// UserName do usuario para login
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; private set; }
    }
}
