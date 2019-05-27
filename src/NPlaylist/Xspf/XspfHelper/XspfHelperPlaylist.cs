using System.Xml.Serialization;

namespace NPlaylist.Xspf.XspfHelper
{
    [XmlRoot(ElementName = "playlist", Namespace = "http://xspf.org/ns/0/")]
    public class XspfHelperPlaylist
    {
        [XmlElement(ElementName = "trackList", Namespace = "http://xspf.org/ns/0/")]
        public XspfHelperTrackList TrackList { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
