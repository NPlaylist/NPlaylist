using System.Xml.Serialization;

namespace NPlaylist.Xspf.Serialization
{
    [XmlRoot(ElementName = "track", Namespace = "http://xspf.org/ns/0/")]
    public class Track
    {
        [XmlElement(ElementName = "title", Namespace = "http://xspf.org/ns/0/")]
        public string Title { get; set; }

        [XmlElement(ElementName = "location", Namespace = "http://xspf.org/ns/0/")]
        public string Location { get; set; }
    }
}
