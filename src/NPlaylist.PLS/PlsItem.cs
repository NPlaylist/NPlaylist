namespace NPlaylist.Pls
{
    public class PlsItem : BasePlaylistItem
    {
        public PlsItem(string path)
            : base(path)
        {
        }

        public PlsItem(IPlaylistItem item)
            : base(item)
        {
        }

        public string Length
        {
            get => Tags.TryGetValue(CommonTags.Length, out var value) ? value : null;
            set => Tags[CommonTags.Length] = value;
        }

        public string Title
        {
            get => Tags.TryGetValue(CommonTags.Title, out var value) ? value : null;
            set => Tags[CommonTags.Title] = value;
        }
    }
}
