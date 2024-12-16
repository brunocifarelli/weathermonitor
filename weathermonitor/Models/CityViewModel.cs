using Newtonsoft.Json;

namespace APIWeather.WebAPP.Models
{
    public class CityViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
