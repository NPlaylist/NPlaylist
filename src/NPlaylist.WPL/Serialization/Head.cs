using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace NPlaylist.Wpl.Serialization
{
    [XmlRoot(ElementName = "head")]
    public class Head
    {
        public Head()
        {
        }

        public Head(WplPlaylist wplPlaylist)
        {
            Meta = ExtractHeadMeta(wplPlaylist);
            Author = wplPlaylist.Author;
            Title = wplPlaylist.Title;
        }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "meta")]
        public List<Meta> Meta { get; } = new List<Meta>();

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        private static List<Meta> ExtractHeadMeta(WplPlaylist playlist)
        {
            return playlist
                .Tags
                .Where(kv =>
                       kv.Key != CommonTags.Author
                    && kv.Key != CommonTags.Title)
                .Select(kv => new Meta { Name = kv.Key, Content = kv.Value })
                .ToList();
        }
    }
}
