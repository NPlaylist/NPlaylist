using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NPlaylist.Asx.AsxParts;

namespace NPlaylist.Asx
{
    public class AsxDeserializer : IPlaylistDeserializer<AsxPlaylist>
    {
        public AsxPlaylist Deserialize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var asxRawData = ConvertToAsxParts(input);
            return ConvertFromRawData(asxRawData);
        }

        private AsxBase ConvertToAsxParts(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(AsxBase));

            using (var reader = XmlReader.Create(new StringReader(input)))
            {
                try
                {
                    return xmlSerializer.Deserialize(reader) as AsxBase;
                }
                catch (Exception)
                {
                    throw new FormatException();
                }
            }
        }

        private AsxPlaylist ConvertFromRawData(AsxBase asxRawData)
        {
            var playlist = new AsxPlaylist();

            AddHead(playlist, asxRawData);
            AddItems(playlist, asxRawData.Entry);

            return playlist;
        }

        private void AddHead(AsxPlaylist playlist, AsxBase asxRawData)
        {
            playlist.Version = asxRawData.Version;
            playlist.Title = asxRawData.Title;
        }

        private void AddItems(AsxPlaylist playlist, IEnumerable<Entry> entries)
        {
            foreach (var item in entries)
            {
                var asxItem = new AsxItem(item.Ref?.Href)
                {
                    Title = item.Title,
                    Author = item.Author,
                    Copyright = item.Copyright
                };

                foreach (var itemTags in item.Param)
                {
                    asxItem.Tags[itemTags.Name] = itemTags.Value;
                }

                playlist.Add(asxItem);
            }
        }
    }
}
