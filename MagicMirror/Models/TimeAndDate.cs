using Microsoft.Extensions.Configuration;
using System;

namespace MagicMirror.Models
{
    public class TimeAndDate
    {
        private DateTime now = DateTime.Now;
        private string timeFormat;
        private string dateFormat;

        public TimeAndDate(IConfiguration config)
        {
            timeFormat = config["TimeAndDate:TimeFormat"];
            dateFormat = config["TimeAndDate:DateFormat"];
        }

        public string TimeFormatted => now.ToString(timeFormat);
        public string DateFormatted => now.ToString(dateFormat);
    }
}
