namespace NPlaylist
{
    public interface IPlaylistSerializer<in T> where T : IPlaylist
    {
        string Serialize(T playlist);
    }
}
