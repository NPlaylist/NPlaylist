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

        protected override PlsPlaylistItem CreateItem(IPlaylistItem item)
        {
            return new PlsPlaylistItem(item);
        }
    }
}
