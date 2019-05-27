using System.Xml.Serialization;

namespace NPlaylist.Wpl.Serialization
{
    [XmlRoot(ElementName = "meta")]
    public class Meta
    {
        [XmlAttribute(AttributeName = "content")]
        public string Content { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
