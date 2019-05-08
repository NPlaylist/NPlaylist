using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPlaylist.Xspf.XspfHelper
{
    [XmlRoot(ElementName = "trackList", Namespace = "http://xspf.org/ns/0/")]
    public class XspfHelperTrackList
    {
        [XmlElement(ElementName = "track", Namespace = "http://xspf.org/ns/0/")]
        public List<XspfHelperTrack> Track { get; } = new List<XspfHelperTrack>();
    }
}
