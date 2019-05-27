using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NPlaylist.Wpl.Serialization;

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

            var rawPlaylist = DeserializeXml(input);
            return ToPlaylist(rawPlaylist);
        }

        private void AddItems(WplPlaylist playlist, Body body)
        {
            foreach (var media in body.Sequence.Media)
            {
                var wplItem = new WplItem(media.Src) { TrackId = media.Tid };
                playlist.Add(wplItem);
            }
        }

        private void AddTags(WplPlaylist playlist, Head head)
        {
            playlist.Title = head.Title;
            playlist.Author = head.Author;

            foreach (var metaTag in head.Meta)
            {
                playlist.Tags[metaTag.Name] = metaTag.Content;
            }
        }

        private object DeserializeToObject(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(Smil));

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

        private Smil DeserializeXml(string input)
        {
            if (!(DeserializeToObject(input) is Smil rawPlaylist))
            {
                throw new FormatException();
            }

            return rawPlaylist;
        }

        private WplPlaylist ToPlaylist(Smil rawPlaylist)
        {
            var playlist = new WplPlaylist();

            AddTags(playlist, rawPlaylist.Head);
            AddItems(playlist, rawPlaylist.Body);

            return playlist;
        }
    }
}
