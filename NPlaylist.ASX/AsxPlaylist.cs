namespace NPlaylist.Asx
{
    public class AsxPlaylist : BasePlaylist<AsxItem>
    {
        public AsxPlaylist()
        {
        }

        public AsxPlaylist(IPlaylist playlist) : base(playlist)
        {
        }

        public string Title
        {
            get => Tags.TryGetValue(CommonTags.Title, out var value) ? value : null;
            set => Tags[CommonTags.Title] = value;
        }

        public string Version
        {
            get => Tags.TryGetValue(CommonTags.Version, out var value) ? value : null;
            set => Tags[CommonTags.Version] = value;
        }

        protected override AsxItem CreateItem(IPlaylistItem item)
        {
            return new AsxItem(item);
        }
    }
}
