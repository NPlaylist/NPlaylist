using System;
using System.Text;
using System.Xml.Serialization;
using NPlaylist.Xspf.Serialization;

namespace NPlaylist.Xspf
{
    public class XspfSerializer : IPlaylistSerializer<XspfPlaylist>
    {
        public string Serialize(XspfPlaylist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            var helperPlaylist = ToPlaylist(playlist);
            return SerializeXml(helperPlaylist);
        }

        private string SerializeXml(Playlist helperPlaylist)
        {
            var xmlSerializer = new XmlSerializer(typeof(Playlist));
            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                xmlSerializer.Serialize(stringWriter, helperPlaylist);

                return stringWriter.ToString();
            }
        }

        private Playlist ToPlaylist(XspfPlaylist playlist)
        {
            var helperPlaylist = new Playlist
            {
                Version = playlist.Version,
                TrackList = new TrackList(),
            };

            foreach (var xspfPlaylistItem in playlist.Items)
            {
                helperPlaylist.TrackList.Track.Add(new Track
                {
                    Title = xspfPlaylistItem.Title,
                    Location = xspfPlaylistItem.Path
                });
            }

            return helperPlaylist;
        }
    }
}
