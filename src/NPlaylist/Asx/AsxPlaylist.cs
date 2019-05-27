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
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        public string Version
        {
            get => Tags.TryGetValue(TagNames.Version, out var value) ? value : null;
            set => Tags[TagNames.Version] = value;
        }

        protected override AsxItem CreateItem(IPlaylistItem item)
        {
            return new AsxItem(item);
        }
    }
}
