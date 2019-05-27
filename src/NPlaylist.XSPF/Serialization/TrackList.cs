using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPlaylist.Xspf.Serialization
{
    [XmlRoot(ElementName = "trackList", Namespace = "http://xspf.org/ns/0/")]
    public class TrackList
    {
        [XmlElement(ElementName = "track", Namespace = "http://xspf.org/ns/0/")]
        public List<Track> Track { get; } = new List<Track>();
    }
}
