using System;
using System.IO;
using System.Linq;

namespace NPlaylist.PLS.PlsParts
{
    public class PlsDeserializer : IPlaylistDeserializer<PlsPlaylist>
    {
        public PlsPlaylist Deserialize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!File.Exists(input))
            {
                throw new FileNotFoundException();
            }
            var playlist = new PlsPlaylist();
            using (StreamReader fstream = new StreamReader(input))
            {
                string line;
                bool IsFirstGap=true;
                var playlistItem = new PlsItem(input);
                while (!fstream.EndOfStream)
                {
                    line = fstream.ReadLine();
                    if (line.Equals(String.Empty) && IsFirstGap)
                    {
                        IsFirstGap = false;
                        continue;
                    }
                    if (line.StartsWith("NumberOfEntries"))
                    {
                        playlist.NumberOfEntries = line.Substring(line.IndexOf('=') + 1);
                        continue;
                    }

                    if (line.StartsWith("Version"))
                    {
                        playlist.Version = line.Substring(line.IndexOf('=') + 1);
                        continue;
                    }
                    if (line.StartsWith("File"))
                    {
                        playlistItem.Path = line.Substring(line.IndexOf('=') + 1);
                        continue;
                    }
                    if (line.StartsWith("Length"))
                    {
                        playlistItem.Length = line.Substring(line.IndexOf('=') + 1);
                        continue;
                    }
                    if (line.StartsWith("Title"))
                    {
                        playlistItem.Title = line.Substring(line.IndexOf('=') + 1);
                        continue;
                    }
                    if (line.Equals(String.Empty))
                    {
                        playlist.Add(new PlsItem(input) { Length = playlistItem.Length, Path = playlistItem.Path, Title = playlistItem.Title });
                        playlistItem.Title = null;
                        playlistItem.Length = null;
                        playlistItem.Path = null;
                    }
                }
            }
            return playlist;
        }
    }
}
