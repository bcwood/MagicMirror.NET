using System;
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

        public IActionResult News()
        {
            int itemsToDisplay = Convert.ToInt32(configuration["News:ItemsToDisplay"]);
            string[] feedUrls = configuration["News:FeedUrls"].Split(',');

            var service = new RssService(feedUrls, itemsToDisplay);
            var items = service.GetRssFeedItems();

            return PartialView("_News", items);
        }

        public IActionResult Rss(string src)
        {
            int itemsToDisplay = Convert.ToInt32(configuration["News:ItemsToDisplay"]);

            var service = new RssService(src, itemsToDisplay);
            var feed = service.GetRssFeed();

            return PartialView("_Rss", feed);
        }
    }
}