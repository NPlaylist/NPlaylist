using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace NPlaylist.Wpl.WplParts
{
    [XmlRoot(ElementName = "head")]
    public class Head
    {
        [XmlElement(ElementName = "meta")]
        public List<Meta> Meta { get; } = new List<Meta>();

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        public Head()
        {
        }

        public Head(WplPlaylist wplPlaylist)
        {
            Meta = ExtractHeadMeta(wplPlaylist);
            Author = wplPlaylist.Author;
            Title = wplPlaylist.Title;
        }

        private static List<Meta> ExtractHeadMeta(WplPlaylist playlist)
        {
            return playlist
                .Tags
                .Where(kv =>
                       kv.Key != TagNames.Author
                    && kv.Key != TagNames.Title)
                .Select(kv => new Meta { Name = kv.Key, Content = kv.Value })
                .ToList();
        }
    }
}
