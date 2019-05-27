namespace NPlaylist.Xspf
{
    public class XspfPlaylist : BasePlaylist<XspfPlaylistItem>
    {
        public string Version
        {
            get => Tags.TryGetValue(TagNames.Version, out var value) ? value : null;
            set => Tags[TagNames.Version] = value;
        }

        public XspfPlaylist()
        {
        }

        public XspfPlaylist(IPlaylist playlist) : base(playlist)
        {
        }

        protected override XspfPlaylistItem CreateItem(IPlaylistItem item)
        {
            return new XspfPlaylistItem(item);
        }
    }
}
