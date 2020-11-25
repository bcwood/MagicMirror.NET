using System.Threading.Tasks;
using Ical.Net;

namespace MagicMirror.Services
{
    public class CalendarService
    {
        public async Task<Calendar> GetCalendarAsync(string url)
        {
            var response = await ApiClient.GetRawAsync(url, null);
            return Calendar.Load(response);
        }
    }
}