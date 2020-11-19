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

        public IActionResult Rss(string src)
        {
            int itemsToDisplay = Convert.ToInt32(configuration["News:ItemsToDisplay"]);

            var service = new RssService(src, itemsToDisplay);
            var feed = service.GetRssFeed();

            return PartialView("_Rss", feed);
        }

        public IActionResult Calendar()
        {
            var service = new CalendarService(configuration);
            var ical = service.GetCalendarAsync().Result;

            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);

            var events = new List<CalendarItem>();

            foreach (var recurrence in ical.RecurringItems)
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

            var calendar = new Calendar();
            calendar.TodaysEvents = events.Where(e => e.StartDate.Date == DateTime.Today)
                                          .ToList();
            calendar.UpcomingEvents = events.Where(e => e.StartDate.Date != DateTime.Today)
                                            .OrderBy(e => e.StartDate)
                                            .ToList();

            return PartialView("_Calendar", calendar);
        }
    }
}