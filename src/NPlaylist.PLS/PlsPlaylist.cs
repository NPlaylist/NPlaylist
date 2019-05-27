namespace NPlaylist.Pls
{
    public class PlsPlaylist : BasePlaylist<PlsItem>
    {
        public PlsPlaylist()
        {
        }

        public PlsPlaylist(IPlaylist playlist)
            : base(playlist)
        {
        }

        public string Version
        {
            get => Tags.TryGetValue(CommonTags.Version, out var value) ? value : null;
            set => Tags[CommonTags.Version] = value;
        }

        protected override PlsItem CreateItem(IPlaylistItem item)
        {
            return new PlsItem(item);
        }
    }
}
