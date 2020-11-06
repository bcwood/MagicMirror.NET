using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MagicMirror.Models;

namespace MagicMirror.Services
{
    public class RssService
    {
        private string[] FeedUrls;
        private int MaxItems = 5;

        public RssService(string[] feedUrls, int maxItems)
        {
            FeedUrls = feedUrls;
            MaxItems = maxItems;
        }

        public List<RssItem> GetRssFeedItems()
        {
            var items = new List<RssItem>();

            foreach (string url in FeedUrls)
            {
                using var reader = XmlReader.Create(url);
                var feed = SyndicationFeed.Load(reader);

                foreach (var item in feed.Items.Take(MaxItems))
                {
                    var rssItem = new RssItem();
                    rssItem.Title = item.Title.Text;
                    rssItem.Summary = item.Summary.Text;
                    rssItem.Link = item.Links.FirstOrDefault()?.Uri;
                    rssItem.PublishDate = item.PublishDate.ToLocalTime();

                    items.Add(rssItem);
                }
            }

            return items.OrderByDescending(i => i.PublishDate.Ticks)
                        .Take(MaxItems)
                        .ToList();
        }
    }
}