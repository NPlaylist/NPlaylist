using System.Xml.Serialization;

namespace NPlaylist.Wpl.Serialization
{
    [XmlRoot(ElementName = "media")]
    public class MediaItem
    {
        public MediaItem()
        {
        }

        public MediaItem(WplItem wplItem)
        {
            Src = wplItem.Path;
            Tid = wplItem.TrackId;
        }

        [XmlAttribute(AttributeName = "src")]
        public string Src { get; set; }

        [XmlAttribute(AttributeName = "tid")]
        public string Tid { get; set; }
    }
}
