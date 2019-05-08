using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using NPlaylist.Asx.AsxParts;

namespace NPlaylist.Asx
{
    public class AsxSerializer : IPlaylistSerializer<AsxPlaylist>
    {
        private static XmlSerializer _xmlSerializer;

        public AsxSerializer()
        {
            _xmlSerializer = new XmlSerializer(typeof(AsxBase));
        }

        public string Serialize(AsxPlaylist playlist)
        {
            if (playlist == null) { 
                return string.Empty;
            }
            var asxObject = ConvertToAsxParts(playlist);
            return ConvertToRawData(asxObject);
        }

        private AsxBase ConvertToAsxParts(AsxPlaylist playlist)
        {
            var objectPlaylist = new AsxBase
            {
                Version = playlist.Version,
                Title = playlist.Title
            };
            objectPlaylist.Entry.AddRange(GetAsxEntries(playlist));
            
            return objectPlaylist;
        }

        private List<Entry> GetAsxEntries(AsxPlaylist playlist)
        {
            return playlist.GenericItems
                .Where(item => !string.IsNullOrWhiteSpace(item.Path))
                .Select(item =>
                {
                    var entry = new Entry
                    {
                        Author = item.Author,
                        Copyright = item.Copyright,
                        Title = item.Title,
                        Ref = new Ref { Href = item.Path }
                    };
                    entry.Param.AddRange(GetEntryTags(item));

                    return entry;
                })
                .ToList();
        }

        private List<ParamItem> GetEntryTags(AsxItem item)
        {
            return item.Tags
                .Where(t => t.Key != TagNames.Path && t.Key != TagNames.Author &&
                            t.Key != TagNames.Copyright && t.Key != TagNames.Title)
                .Select(itemTags => new ParamItem {Name = itemTags.Key, Value = itemTags.Value}).ToList();
        }

        private string ConvertToRawData(AsxBase asxObject)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                try
                {
                    _xmlSerializer.Serialize(writer, asxObject, emptyNamespaces);
                    return stream.ToString();
                }
                catch (Exception)
                {
                    throw new FormatException();
                }
            }
        }
    }
}
