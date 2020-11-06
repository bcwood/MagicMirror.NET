using System;

namespace MagicMirror.Models
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public Uri Link { get; internal set; }
        public DateTimeOffset PublishDate { get; set; }

        public string LinkDomain => Link.Host.Replace("www.", "");

        public string RelativeDate
        {
            get
            {
                var timespan = DateTime.Now - PublishDate.ToLocalTime();

                if (timespan.TotalSeconds < 60)
                    return "now";

                int value;
                string units;

                if (timespan.TotalMinutes < 60)
                {
                    value = (int) Math.Round(timespan.TotalMinutes);
                    units = "minute";
                }
                else if (timespan.TotalHours < 24)
                {
                    value = (int)Math.Round(timespan.TotalHours);
                    units = "hour";
                }
                else
                {
                    value = (int) Math.Round(timespan.TotalDays);
                    units = "day";
                }

                return $"{value} {units}{(value > 1 ? "s" : "")} ago";
            }
        }        
    }
}
