using System.Collections.Generic;

namespace NPlaylist
{
    public interface IPlaylistItem
    {
        IDictionary<string, string> Tags { get; }
        string Path { get; }
    }
}
