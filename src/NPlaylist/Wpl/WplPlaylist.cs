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
            get => Tags.TryGetValue(CommonTags.Title, out var value) ? value : null;
            set => Tags[CommonTags.Title] = value;
        }

        public string Author
        {
            get => Tags.TryGetValue(CommonTags.Author, out var value) ? value : null;
            set => Tags[CommonTags.Author] = value;
        }

        protected override WplItem CreateItem(IPlaylistItem item)
        {
            return new WplItem(item);
        }
    }
}
