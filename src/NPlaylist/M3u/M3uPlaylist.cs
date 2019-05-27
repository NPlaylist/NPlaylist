namespace NPlaylist.M3u
{
    public class M3uPlaylist : BasePlaylist<M3uItem>
    {
        public M3uPlaylist()
        {
        }

        public M3uPlaylist(IPlaylist playlist) : base(playlist)
        {
        }

        protected override M3uItem CreateItem(IPlaylistItem item)
        {
            return new M3uItem(item);
        }
    }
}
