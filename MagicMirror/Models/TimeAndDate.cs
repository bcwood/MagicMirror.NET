using System;

namespace MagicMirror.Models
{
    public class TimeAndDate
    {
        public readonly string TimeFormatted;
        public readonly string DateFormatted;

        public TimeAndDate(string timeFormat, string dateFormat)
        {
            var now = DateTime.Now;
            TimeFormatted = now.ToString(timeFormat);
            DateFormatted = now.ToString(dateFormat);
        }
    }
}
