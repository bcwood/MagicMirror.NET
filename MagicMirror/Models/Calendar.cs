using System.Collections.Generic;

namespace MagicMirror.Models
{
    public class Calendar
    {
        public List<CalendarItem> TodaysEvents { get; set; }
        public List<CalendarItem> UpcomingEvents { get; set; }
    }
}
