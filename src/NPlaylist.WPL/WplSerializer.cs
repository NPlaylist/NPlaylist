using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NPlaylist.Wpl.Serialization;

namespace NPlaylist.Wpl
{
    public class WplSerializer : IPlaylistSerializer<WplPlaylist>
    {
        public string Serialize(WplPlaylist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            var rawPlaylist = new Smil(playlist);
            var xmlPlaylist = SerializeXml(rawPlaylist);
            return ReplaceXmlInitialTagWithWplTag(xmlPlaylist);
        }

        private string SerializeXml(Smil rawPlaylist)
        {
            var xmlSerializer = new XmlSerializer(typeof(Smil));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, rawPlaylist);
                return textWriter.ToString();
            }
        }

        private string ReplaceXmlInitialTagWithWplTag(string playlistXml)
        {
            return Regex.Replace(playlistXml, @"^(<\?)(xml)", "$1wpl");
        }
    }
}
