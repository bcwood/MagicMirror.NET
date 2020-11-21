namespace MagicMirror.ViewModels
{
    public class RssViewModel
    {
        public string Url { get; set; }
        public string Refresh { get; set; }

        public RssViewModel(string url, string refresh)
        {
            Url = url;
            Refresh = refresh;
        }
    }
}
