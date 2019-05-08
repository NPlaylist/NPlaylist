using System;
using System.Text;
using System.Xml.Serialization;
using NPlaylist.Xspf.XspfHelper;

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

            var helperPlaylist = FromXspfToHelper(playlist);
            return XspfHelperToString(helperPlaylist);
        }

        private string XspfHelperToString(XspfHelperPlaylist helperPlaylist)
        {
            var xmlSerializer = new XmlSerializer(typeof(XspfHelperPlaylist));
            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                xmlSerializer.Serialize(stringWriter, helperPlaylist);

                return stringWriter.ToString();
            }
        }

        private XspfHelperPlaylist FromXspfToHelper(XspfPlaylist playlist)
        {
            var helperPlaylist = new XspfHelperPlaylist
            {
                Version = playlist.Version,
                TrackList = new XspfHelperTrackList(),
            };

            foreach (var xspfPlaylistItem in playlist.GenericItems)
            {
                helperPlaylist.TrackList.Track.Add(new XspfHelperTrack
                {
                    Title = xspfPlaylistItem.Title,
                    Location = xspfPlaylistItem.Path
                });
            }

            return helperPlaylist;
        }
    }
}
