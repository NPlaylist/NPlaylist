using System.Xml.Serialization;

namespace NPlaylist.Asx.AsxParts
{
    [XmlRoot(ElementName = "ref")]
    public class Ref
    {
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
    }
}
