using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MagicMirror.Models;

namespace MagicMirror.Services
{
    public class RssService
    {
        public RssFeed GetRssFeed(string url, int maxItems)
        {
            var rssFeed = new RssFeed();

            var synFeed = GetSyndicationFeed(url);
            rssFeed.Title = synFeed.Title.Text;
            rssFeed.Items = GetSyndicationFeedItems(synFeed, maxItems);

            return rssFeed;
        }

        private SyndicationFeed GetSyndicationFeed(string url)
        {
            var reader = XmlReader.Create(url);
            return SyndicationFeed.Load(reader);
        }

        private List<RssItem> GetSyndicationFeedItems(SyndicationFeed feed, int maxItems)
        {
            var items = new List<RssItem>();

            foreach (var item in feed.Items.Take(maxItems))
            {
                var rssItem = new RssItem();
                rssItem.Title = item.Title.Text;
                rssItem.Summary = item.Summary.Text;
                rssItem.Link = item.Links.FirstOrDefault()?.Uri;
                rssItem.PublishDate = item.PublishDate.ToLocalTime();

                items.Add(rssItem);
            }

            return items.OrderByDescending(i => i.PublishDate).ToList();
        }
    }
}