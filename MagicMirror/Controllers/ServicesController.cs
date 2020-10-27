using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MagicMirror.Models;
using MagicMirror.Services;

namespace MagicMirror.Controllers
{
    public class ServicesController : Controller
    {
        private IConfiguration configuration;

        public ServicesController(IConfiguration config)
        {
            configuration = config;
        }

        public IActionResult TimeAndDate()
        {
            var timeAndDate = new TimeAndDate(configuration);
            return PartialView("_TimeAndDate", timeAndDate);
        }

        public IActionResult Weather()
        {
            var service = new WeatherService(configuration);
            
            var weather = service.GetWeatherAsync().Result;
            // skip the first day in the forecast (today), and only take 5 days
            weather.Forecast = weather.Forecast.Skip(1).Take(5).ToList();
            return PartialView("_Weather", weather);
        }
    }
}