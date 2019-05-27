using System.Xml.Serialization;

namespace NPlaylist.Xspf.XspfHelper
{
    [XmlRoot(ElementName = "track", Namespace = "http://xspf.org/ns/0/")]
    public class XspfHelperTrack
    {
        [XmlElement(ElementName = "title", Namespace = "http://xspf.org/ns/0/")]
        public string Title { get; set; }

        [XmlElement(ElementName = "location", Namespace = "http://xspf.org/ns/0/")]
        public string Location { get; set; }
    }
}
