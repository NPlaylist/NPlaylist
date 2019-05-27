using System.Globalization;

namespace NPlaylist.M3u
{
    public class M3uItem : BasePlaylistItem
    {
        public M3uItem(string path, decimal duration = 0) : base(path)
        {
            Duration = duration;
        }

        public M3uItem(IPlaylistItem item) : base(item)
        {
        }

        public decimal Duration
        {
            get
            {
                if (!Tags.TryGetValue(CommonTags.Length, out var valueStr))
                {
                    return 0;
                }

                return decimal.TryParse(valueStr, out var decimalValue) ? decimalValue : 0;
            }

            set => Tags[CommonTags.Length] = value.ToString(CultureInfo.InvariantCulture);
        }

        public string Title
        {
            get
            {
                var title = Tags.TryGetValue(CommonTags.Title, out var value) ? value : null;
                return title?.Trim();
            }

            set => Tags[CommonTags.Title] = value;
        }
    }
}
