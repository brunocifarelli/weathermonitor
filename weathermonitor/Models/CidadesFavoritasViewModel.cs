using Newtonsoft.Json;

namespace APIWeather.WebAPP.Models
{
    public class CidadesFavoritasViewModel
    {
        /// <summary>
        /// Chave primária da cidade favorita
        /// </summary>
        public Guid FavoriteCityId { get; set; }
        /// <summary>
        /// Nome da cidade favorita
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id do usuário que favoritou a cidade
        /// </summary>
        public Guid UsuarioId { get; set; }
        /// <summary>
        /// Latitude da cidade favorita
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// Longitude da cidade favorita
        /// </summary>
        public string Lon { get; set; }

        public string Id { get; set; }
    }
}
