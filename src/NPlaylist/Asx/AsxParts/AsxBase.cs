using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPlaylist.Asx.AsxParts
{
    [XmlRoot(ElementName = "asx")]
    public class AsxBase
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "entry")]
        public List<Entry> Entry { get; } = new List<Entry>();

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
