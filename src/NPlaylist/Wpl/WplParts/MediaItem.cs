using System.Xml.Serialization;

namespace NPlaylist.Wpl.WplParts
{
    [XmlRoot(ElementName = "media")]
    public class MediaItem
    {
        [XmlAttribute(AttributeName = "src")]
        public string Src { get; set; }

        [XmlAttribute(AttributeName = "tid")]
        public string Tid { get; set; }

        public MediaItem()
        {
        }

        public MediaItem(WplItem wplItem)
        {
            Src = wplItem.Path;
            Tid = wplItem.TrackId;
        }
    }
}
