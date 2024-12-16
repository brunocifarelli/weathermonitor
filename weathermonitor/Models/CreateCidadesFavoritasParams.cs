using Newtonsoft.Json;

namespace APIWeather.WebAPP.Models
{
    public class CreateCidadesFavoritasParams
    {
        /// <summary>
        /// Nome da cidade favorita
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Latitude da cidade favorita
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Longitude da cidade favorita
        /// </summary>
        public string Longitude { get; set; }
    }

}
