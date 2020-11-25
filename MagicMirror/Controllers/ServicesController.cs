using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MagicMirror.Models;
using MagicMirror.Services;
using Ical.Net.CalendarComponents;

namespace MagicMirror.Controllers
{
    public class ServicesController : Controller
    {
        private IConfiguration _configuration;

        public ServicesController(IConfiguration config)
        {
            _configuration = config;
        }

        public IActionResult TimeAndDate()
        {
            string timeFormat = _configuration["TimeAndDate:TimeFormat"];
            string dateFormat = _configuration["TimeAndDate:DateFormat"];

            var timeAndDate = new TimeAndDate(timeFormat, dateFormat);
            return PartialView("_TimeAndDate", timeAndDate);
        }

        public IActionResult Weather()
        {
            string apiKey = _configuration["Weather:ApiKey"];
            string units = _configuration["Weather:Units"];

            var service = new WeatherService(apiKey, units);

            string latitude = _configuration["Weather:Latitude"];
            string longitude = _configuration["Weather:Longitude"];

            var weather = service.GetWeatherAsync(latitude, longitude).Result;
            // skip the first day in the forecast (today), and only take 5 days
            weather.Forecast = weather.Forecast.Skip(1).Take(5).ToList();
            return PartialView("_Weather", weather);
        }

        public IActionResult Rss(string src, int maxItems = 5)
        {
            var service = new RssService();
            var feed = service.GetRssFeed(src, maxItems);

            return PartialView("_Rss", feed);
        }

        public IActionResult Calendar()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);

            var service = new CalendarService();
            var events = new List<CalendarItem>();

            string[] calendarUrls = _configuration.GetSection("Calendars").Get<string[]>();

            foreach (string url in calendarUrls)
            {
                var calendar = service.GetCalendarAsync(url).Result;
            
                foreach (var recurrence in calendar.RecurringItems)
                {
                    foreach (var occurrence in recurrence.GetOccurrences(startDate, endDate))
                    {
                        var calendarEvent = occurrence.Source as CalendarEvent;

                        foreach (var eventOccurrence in calendarEvent.GetOccurrences(startDate, endDate))
                        {
                            var subEvent = eventOccurrence.Source as CalendarEvent;

                            events.Add(new CalendarItem
                            {
                                Title = subEvent.Summary,
                                StartDate = eventOccurrence.Period.StartTime.AsSystemLocal,
                                EndDate = eventOccurrence.Period.EndTime.AsSystemLocal,
                                IsAllDay = subEvent.IsAllDay
                            });
                        }
                    }
                }
            }

            var viewModel = new Calendar();
            viewModel.TodaysEvents = events.Where(e => e.StartDate.Date == DateTime.Today)
                                           .ToList();
            viewModel.UpcomingEvents = events.Where(e => e.StartDate.Date != DateTime.Today)
                                             .OrderBy(e => e.StartDate)
                                             .ToList();

            return PartialView("_Calendar", viewModel);
        }
    }
}