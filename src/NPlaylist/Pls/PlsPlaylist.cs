namespace NPlaylist.Pls
{
    public class PlsPlaylist : BasePlaylist<PlsItem>
    {
        public string Version
        {
            get => Tags.TryGetValue(CommonTags.Version, out var value) ? value : null;
            set => Tags[CommonTags.Version] = value;
        }

        public PlsPlaylist()
        {
        }

        public PlsPlaylist(IPlaylist playlist) : base(playlist)
        {
        }

        protected override PlsItem CreateItem(IPlaylistItem item)
        {
            return new PlsItem(item);
        }
    }
}
