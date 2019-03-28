namespace NPlaylist.Pls
{
    public class PlsPlaylistItem : BasePlaylistItem
    {
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
        public PlsPlaylistItem(string path) : base(path)
        {
        }
    }
}
