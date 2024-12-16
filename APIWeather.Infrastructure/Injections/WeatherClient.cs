
using System.Net;

namespace APIWeather.Infrastructure.Injections
{
    public class WeatherClient
    {
        public HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var urlBaseAddress = $"http://api.openweathermap.org/";

            client.BaseAddress = new Uri(urlBaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();

            return client;
        }
    }

}
