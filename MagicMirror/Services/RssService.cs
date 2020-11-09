using System;
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

        public RssService(string feedUrl, int maxItems)
        {
            FeedUrls = new string[] { feedUrl };
            MaxItems = maxItems;
        }

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
                var feed = GetSyndicationFeed(url);

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

        public RssFeed GetRssFeed()
        {
            var rssFeed = new RssFeed();

            var synFeed = GetSyndicationFeed(FeedUrls[0]);
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

            return items;
        }
    }
}