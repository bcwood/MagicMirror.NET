using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Ical.Net;

namespace MagicMirror.Services
{
    public class CalendarService
    {
        private string calendarUrl;

        public CalendarService(IConfiguration config)
        {
            calendarUrl = config["Calendar:Url"];
        }

        public async Task<Calendar> GetCalendarAsync()
        {
            var response = await ApiClient.GetRawAsync(calendarUrl, null);
            return Calendar.Load(response);
        }
    }
}