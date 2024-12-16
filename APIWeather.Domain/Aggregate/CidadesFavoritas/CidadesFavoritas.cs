using APIWeather.Domain.Validate;

namespace APIWeather.Domain.Aggregate.CidadesFavoritas
{
    public class CidadesFavoritas
    {
        private CidadesFavoritas() { }

        public CidadesFavoritas(string nome, Guid usuarioId, string latitude, string longitude, string idCity)
        {
            Guard.Validate(errorIf =>
            {
                errorIf
                    .IsEmptyGuid(usuarioId, nameof(usuarioId),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado", nameof(usuarioId)))
                    .IsNullOrEmptyString(nome, nameof(nome),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado", nameof(nome)))
                    .IsNullOrEmptyString(latitude, nameof(latitude),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado", nameof(latitude)))
                    .IsNullOrEmptyString(longitude, nameof(longitude),
                        string.Format("O parâmetro {0} informado está inválido ou não foi informado",
                            nameof(longitude)))
                    .IsNullOrEmptyString(idCity, nameof(idCity),
                    string.Format("O parâmetro {0} informado está inválido ou não foi informado",
                        nameof(idCity)));
            });

            Id = Guid.NewGuid();
            CityId = idCity;
            Nome = nome;
            UsuarioId = usuarioId;
            Latitude = latitude;
            Longitude = longitude;
        }
        /// <summary>
        /// Chave primária da cidade favorita
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Nome da cidade favorita
        /// </summary>
        public string Nome { get; private set; }
        /// <summary>
        /// Id do usuário que favoritou a cidade
        /// </summary>
        public Guid UsuarioId { get; private set; }
        /// <summary>
        /// Latitude da cidade favorita
        /// </summary>
        public string Latitude { get; private set; }
        /// <summary>
        /// Longitude da cidade favorita
        /// </summary>
        public string Longitude { get; private set; }
        /// <summary>
        /// Id da cidade na api.
        /// </summary>
        public string CityId { get; set; }
    }


}
