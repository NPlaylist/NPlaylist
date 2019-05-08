using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPlaylist.Wpl
{
    public class WplDeserializer : IPlaylistDeserializer<WplPlaylist>
    {
        public WplPlaylist Deserialize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var rawPlaylist = DeserializeToRawPlaylist(input);
            return ConvertFromRawPlaylist(rawPlaylist);
        }

        private WplParts.RawPlaylist DeserializeToRawPlaylist(string input)
        {
            if (!(DeserializeToObject(input) is WplParts.RawPlaylist rawPlaylist))
            {
                throw new FormatException();
            }

            return rawPlaylist;
        }

        private object DeserializeToObject(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(WplParts.RawPlaylist));

            using (var reader = XmlReader.Create(new StringReader(input)))
            {
                try
                {
                    return xmlSerializer.Deserialize(reader);
                }
                catch (InvalidOperationException)
                {
                    throw new FormatException();
                }
            }
        }

        private WplPlaylist ConvertFromRawPlaylist(WplParts.RawPlaylist rawPlaylist)
        {
            var playlist = new WplPlaylist();

            AddTags(playlist, rawPlaylist.Head);
            AddItems(playlist, rawPlaylist.Body);

            return playlist;
        }

        private void AddTags(WplPlaylist playlist, WplParts.Head head)
        {
            playlist.Title = head.Title;
            playlist.Author = head.Author;

            foreach (var metaTag in head.Meta)
            {
                playlist.Tags[metaTag.Name] = metaTag.Content;
            }
        }

        private void AddItems(WplPlaylist playlist, WplParts.Body body)
        {
            foreach (var media in body.Sequence.Media)
            {
                var wplItem = new WplItem(media.Src) { TrackId = media.Tid };
                playlist.Add(wplItem);
            }
        }
    }
}
