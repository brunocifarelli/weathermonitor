using Newtonsoft.Json;

namespace APIWeather.WebAPP.Models
{
    public class WeatherForecastViewModel
    {
        [JsonProperty("cod")] public string Cod { get; set; }

        [JsonProperty("message")] public int Message { get; set; }

        [JsonProperty("cnt")] public int Cnt { get; set; }

        [JsonProperty("list")] public List<WeatherData> List { get; set; }

        [JsonProperty("city")] public City City { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("dt")] public int Dt { get; set; }

        [JsonProperty("main")] public MainWeather Main { get; set; }

        [JsonProperty("weather")] public List<Weather> Weather { get; set; }

        [JsonProperty("clouds")] public Clouds Clouds { get; set; }

        [JsonProperty("wind")] public Wind Wind { get; set; }

        [JsonProperty("visibility")] public int Visibility { get; set; }

        [JsonProperty("pop")] public double Pop { get; set; }

        [JsonProperty("sys")] public Sys Sys { get; set; }

        [JsonProperty("dt_txt")] public DateTime DtTxt { get; set; }

    }

    public class MainWeather
    {
        [JsonProperty("temp")] public double Temp { get; set; }

        [JsonProperty("feels_like")] public double FeelsLike { get; set; }

        [JsonProperty("temp_min")] public string TempMin { get; set; }

        [JsonProperty("temp_max")] public string TempMax { get; set; }

        [JsonProperty("pressure")] public int Pressure { get; set; }

        [JsonProperty("sea_level")] public int SeaLevel { get; set; }

        [JsonProperty("grnd_level")] public int GrndLevel { get; set; }

        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("temp_kf")] public double TempKf { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")] public int All { get; set; }
    }


    public class City
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("coord")] public Coord Coord { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("population")] public int Population { get; set; }

        [JsonProperty("timezone")] public int Timezone { get; set; }

        [JsonProperty("sunrise")] public int Sunrise { get; set; }

        [JsonProperty("sunset")] public int Sunset { get; set; }
    }

    public class WeatherForecastData
    {
        public string Date { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }


}
