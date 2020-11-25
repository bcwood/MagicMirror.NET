using System.Threading.Tasks;
using MagicMirror.Models;

namespace MagicMirror.Services
{
    // More info: https://openweathermap.org/api/one-call-api
    public class WeatherService
    {
        private const string BASE_URL = "http://api.openweathermap.org/data/2.5";
        
        private string _apiKey;
        private string _units;
        
        public WeatherService(string apiKey, string units)
        {
            _apiKey = apiKey;
            _units = units;
        }

        public async Task<Weather> GetWeatherAsync(string latitude, string longitude)
        {
            string endpointUrl = $"{BASE_URL}/onecall?lat={latitude}&lon={longitude}&units={_units}&exclude=minutely,hourly,alerts&appid={_apiKey}";
            return await ApiClient.GetAsync<Weather>(endpointUrl);
        }
    }
}