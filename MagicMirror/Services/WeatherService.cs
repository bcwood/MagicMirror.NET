using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MagicMirror.Models;

namespace MagicMirror.Services
{
    // More info: https://openweathermap.org/api/one-call-api
    public class WeatherService
    {
        private const string BASE_URL = "http://api.openweathermap.org/data/2.5";
        
        private string apiKey;
        private string latitude;
        private string longitude;

        public WeatherService(IConfiguration config)
        {
            apiKey = config["Weather:ApiKey"];
            latitude = config["Weather:Latitude"];
            longitude = config["Weather:Longitude"];
        }

        public async Task<Weather> GetWeatherAsync()
        {
            string endpointUrl = $"{BASE_URL}/onecall?lat={latitude}&lon={longitude}&appid={apiKey}&units=imperial&exclude=minutely,hourly";
            return await ApiClient.GetAsync<Weather>(endpointUrl);
        }
    }
}