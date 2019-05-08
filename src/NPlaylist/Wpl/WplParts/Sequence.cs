using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPlaylist.Wpl.WplParts
{
    [XmlRoot(ElementName = "seq")]
    public class Sequence
    {
        [XmlElement(ElementName = "media")]
        public List<MediaItem> Media { get; } = new List<MediaItem>();
    }
}
