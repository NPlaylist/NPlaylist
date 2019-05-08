using System.Xml.Serialization;

namespace NPlaylist.Wpl.WplParts
{
    [XmlRoot(ElementName = "smil")]
    public class RawPlaylist
    {
        [XmlElement(ElementName = "head")]
        public Head Head { get; set; }

        [XmlElement(ElementName = "body")]
        public Body Body { get; set; }

        public RawPlaylist()
        {
        }

        public RawPlaylist(WplPlaylist wplPlaylist)
        {
            Head = new Head(wplPlaylist);
            Body = new Body(wplPlaylist.GenericItems);
        }
    }
}
