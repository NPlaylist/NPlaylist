using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NPlaylist.Xspf.Serialization;

namespace NPlaylist.Xspf
{
    public class XspfDeserializer : IPlaylistDeserializer<XspfPlaylist>
    {
        public XspfPlaylist Deserialize(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var helperPlaylist = DeserializeXml(input);
            return ToPlaylist(helperPlaylist);
        }

        private Playlist DeserializeXml(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(Playlist));

            using (var reader = XmlReader.Create(new StringReader(input)))
            {
                try
                {
                    return xmlSerializer.Deserialize(reader) as Playlist;
                }
                catch (InvalidOperationException e)
                {
                    throw new FormatException("Unable to deserialize XSPF file format.", e);
                }
            }
        }

        private XspfPlaylist ToPlaylist(Playlist playlist)
        {
            var xspfPlaylist = new XspfPlaylist
            {
                Version = playlist.Version
            };

            foreach (var track in playlist.TrackList.Track)
            {
                xspfPlaylist.Add(new XspfPlaylistItem(track.Location)
                {
                    Title = track.Title
                });
            }
            return xspfPlaylist;
        }
    }
}
