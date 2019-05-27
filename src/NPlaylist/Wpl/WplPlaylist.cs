namespace NPlaylist.Wpl
{
    public class WplPlaylist : BasePlaylist<WplItem>
    {
        public WplPlaylist() : base()
        {
        }

        public WplPlaylist(IPlaylist playlist) : base(playlist)
        {
        }

        public string Title
        {
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        public string Author
        {
            get => Tags.TryGetValue(TagNames.Author, out var value) ? value : null;
            set => Tags[TagNames.Author] = value;
        }

        protected override WplItem CreateItem(IPlaylistItem item)
        {
            return new WplItem(item);
        }
    }
}
