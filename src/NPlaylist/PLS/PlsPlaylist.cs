using System.Configuration;

namespace NPlaylist.PLS.PlsParts
{
    public class PlsPlaylist : BasePlaylist<PlsItem>
    {
        public readonly string playlistHeader = "[playlist]";

        public string NumberOfEntries
        {
            get => Tags.TryGetValue(PlsTagNames.NumbersOfEntries, out var value) ? value : null;
            set => Tags[PlsTagNames.NumbersOfEntries] = value;
        }

        public string Version
        {
            get => Tags.TryGetValue(PlsTagNames.Version, out var value) ? value : null;
            set => Tags[PlsTagNames.Version] = value;
        }
    }
}
