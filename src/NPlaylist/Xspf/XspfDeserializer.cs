using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NPlaylist.Xspf.XspfHelper;

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

            var helperPlaylist = StringToXspfHelper(input);
            return ConvertToXspfPlaylist(helperPlaylist);
        }

        private XspfHelperPlaylist StringToXspfHelper(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(XspfHelperPlaylist));

            XspfHelperPlaylist xspfHelperObj;

            using (var reader = XmlReader.Create(new StringReader(input)))
            {
                try
                {
                    xspfHelperObj = xmlSerializer.Deserialize(reader) as XspfHelperPlaylist;
                    return xspfHelperObj;
                }
                catch (InvalidOperationException)
                {
                    throw new FormatException();
                }
            }
        }

        private XspfPlaylist ConvertToXspfPlaylist(XspfHelperPlaylist playlist)
        {
            var xspfPlaylist = new XspfPlaylist();
            xspfPlaylist.Version = playlist.Version;
            
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
