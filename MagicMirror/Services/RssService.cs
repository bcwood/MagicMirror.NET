using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MagicMirror.Models;

namespace MagicMirror.Services
{
    public class RssService
    {
        private string Url;
        private int MaxItems;

        public RssService(string url, int maxItems)
        {
            Url = url;
            MaxItems = maxItems;
        }

        public RssFeed GetRssFeed()
        {
            var rssFeed = new RssFeed();

            var synFeed = GetSyndicationFeed(Url);
            rssFeed.Title = synFeed.Title.Text;
            rssFeed.Items = GetSyndicationFeedItems(synFeed);

            return rssFeed;
        }

        private SyndicationFeed GetSyndicationFeed(string url)
        {
            var reader = XmlReader.Create(url);
            return SyndicationFeed.Load(reader);
        }

        private List<RssItem> GetSyndicationFeedItems(SyndicationFeed feed)
        {
            var items = new List<RssItem>();

            foreach (var item in feed.Items.Take(MaxItems))
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