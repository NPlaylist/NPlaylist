namespace NPlaylist.Wpl
{
    public class WplItem : BasePlaylistItem
    {
        public WplItem(string path) : base(path)
        {
        }

        public WplItem(IPlaylistItem item) : base(item)
        {
        }

        public string TrackId
        {
            get => Tags.TryGetValue(TagNames.TrackId, out var value) ? value : null;
            set => Tags[TagNames.TrackId] = value;
        }
    }
}
