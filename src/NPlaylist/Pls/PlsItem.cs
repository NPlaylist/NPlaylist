namespace NPlaylist.Pls
{
    public class PlsItem : BasePlaylistItem
    {
        public PlsItem(string path) : base(path)
        {
        }

        public PlsItem(IPlaylistItem item) : base(item)
        {
        }

        public string Title
        {
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        public string Length
        {
            get => Tags.TryGetValue(TagNames.Length, out var value) ? value : null;
            set => Tags[TagNames.Length] = value;
        }
    }
}
