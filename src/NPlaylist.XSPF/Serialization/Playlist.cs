using System.Xml.Serialization;

namespace NPlaylist.Xspf.Serialization
{
    [XmlRoot(ElementName = "playlist", Namespace = "http://xspf.org/ns/0/")]
    public class Playlist
    {
        [XmlElement(ElementName = "trackList", Namespace = "http://xspf.org/ns/0/")]
        public TrackList TrackList { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
