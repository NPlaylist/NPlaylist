namespace NPlaylist.Pls
{
    public class PlsPlaylist : BasePlaylist<PlsItem>
    {
        public string Version
        {
            get => Tags.TryGetValue(TagNames.Version, out var value) ? value : null;
            set => Tags[TagNames.Version] = value;
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
