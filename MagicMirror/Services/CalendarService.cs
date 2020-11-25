using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ical.Net;

namespace MagicMirror.Services
{
    public class CalendarService
    {
        private string[] calendarUrls;

        public CalendarService(IConfiguration config)
        {
            calendarUrls = config.GetSection("Calendars").Get<string[]>();
        }

        public async Task<List<Calendar>> GetCalendarsAsync()
        {
            var calendars = new List<Calendar>();

            foreach (string url in calendarUrls)
            {
                var response = await ApiClient.GetRawAsync(url, null);
                calendars.Add(Calendar.Load(response));
            }

            return calendars;
        }
    }
}