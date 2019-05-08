using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace NPlaylist.Wpl.WplParts
{
    [XmlRoot(ElementName = "body")]
    public class Body
    {
        [XmlElement(ElementName = "seq")]
        public Sequence Sequence { get; set; }

        public Body()
        {
        }

        public Body(IEnumerable<WplItem> wplItems)
        {
            var media = wplItems.Select(x => new MediaItem(x));

            Sequence = new Sequence();
            Sequence.Media.AddRange(media.ToList());
        }
    }
}
