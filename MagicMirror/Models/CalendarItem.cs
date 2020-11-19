using System;

namespace MagicMirror.Models
{
    public class CalendarItem
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAllDay { get; internal set; }
    }
}
