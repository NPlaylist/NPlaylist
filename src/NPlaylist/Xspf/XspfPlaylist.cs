namespace NPlaylist.Xspf
{
    public class XspfPlaylist : BasePlaylist<XspfPlaylistItem>
    {
        public string Version
        {
            get => Tags.TryGetValue(CommonTags.Version, out var value) ? value : null;
            set => Tags[CommonTags.Version] = value;
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
