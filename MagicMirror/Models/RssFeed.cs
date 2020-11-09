using System.Collections.Generic;

namespace MagicMirror.Models
{
    public class RssFeed
    {
        public string Title { get; set; }
        public List<RssItem> Items { get; set; }
    }
}
