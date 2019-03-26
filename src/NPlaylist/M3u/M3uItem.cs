using System.Globalization;

namespace NPlaylist.M3u
{
    public class M3uItem : BasePlaylistItem
    {
        private const decimal defaultDuration = 0;

        public M3uItem(string path, decimal duration) : base(path)
        {
            Duration = duration;
        }

        public decimal Duration
        {
            get => ExtractDuraction();
            set => Tags[TagNames.Length] = value.ToString(CultureInfo.InvariantCulture);
        }

        public string Title
        {
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        private decimal ExtractDuraction()
        {
            if (!Tags.TryGetValue(TagNames.Length, out var valueStr))
            {
                return defaultDuration;
            }

            return decimal.TryParse(valueStr, out var decimalValue) ? decimalValue : defaultDuration;
        }
    }
}
