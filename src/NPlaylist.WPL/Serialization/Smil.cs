using System.Xml.Serialization;

namespace NPlaylist.Wpl.Serialization
{
    [XmlRoot(ElementName = "smil")]
    public class Smil
    {
        public Smil()
        {
        }

        public Smil(WplPlaylist wplPlaylist)
        {
            Head = new Head(wplPlaylist);
            Body = new Body(wplPlaylist.Items);
        }

        [XmlElement(ElementName = "body")]
        public Body Body { get; set; }

        [XmlElement(ElementName = "head")]
        public Head Head { get; set; }
    }
}
