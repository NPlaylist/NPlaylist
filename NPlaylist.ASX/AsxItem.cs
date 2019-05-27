namespace NPlaylist.Asx
{
    public class AsxItem : BasePlaylistItem
    {
        public AsxItem(string path) : base(path)
        {
        }

        public AsxItem(IPlaylistItem item) : base(item)
        {
        }

        public string Title
        {
            get => Tags.TryGetValue(CommonTags.Title, out var value) ? value : null;
            set => Tags[CommonTags.Title] = value;
        }

        public string Author
        {
            get => Tags.TryGetValue(CommonTags.Author, out var value) ? value : null;
            set => Tags[CommonTags.Author] = value;
        }

        public string Copyright
        {
            get => Tags.TryGetValue(CommonTags.Copyright, out var value) ? value : null;
            set => Tags[CommonTags.Copyright] = value;
        }
    }
}
