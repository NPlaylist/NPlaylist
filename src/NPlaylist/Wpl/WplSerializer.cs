using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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

            var rawPlaylist = new WplParts.RawPlaylist(playlist);
            var xmlPlaylist = RawPlaylistToStr(rawPlaylist);
            return ReplaceXmlInitialTagWithWplTag(xmlPlaylist);
        }

        private string RawPlaylistToStr(WplParts.RawPlaylist rawPlaylist)
        {
            var xmlSerializer = new XmlSerializer(typeof(WplParts.RawPlaylist));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, rawPlaylist);
                return textWriter.ToString();
            }
        }

        /*
         * <?xml ...> to <?wpl ...>
        */

        private string ReplaceXmlInitialTagWithWplTag(string playlistXml)
        {
            return Regex.Replace(playlistXml, @"^(<\?)(xml)", "$1wpl");
        }
    }
}
