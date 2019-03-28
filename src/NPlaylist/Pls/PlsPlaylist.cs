namespace NPlaylist.Pls
{
    public class PlsPlaylist : BasePlaylist<PlsPlaylistItem>
    {
        public string Version
        {
            get => Tags.TryGetValue(TagNames.Version, out var value) ? value : null;
            set => Tags[TagNames.Version] = value;
        }

        public PlsPlaylist()
        {
        }
    }
}
