namespace NPlaylist
{
    public interface IPlaylistDeserializer<out T>
        where T : IPlaylist
    {
        T Deserialize(string input);
    }
}
