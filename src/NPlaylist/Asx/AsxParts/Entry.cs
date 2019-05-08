using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPlaylist.Asx.AsxParts
{
    [XmlRoot(ElementName = "entry")]
    public class Entry
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "ref")]
        public Ref Ref { get; set; }

        [XmlElement(ElementName = "param")]
        public List<ParamItem> Param { get; } = new List<ParamItem>();

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "copyright")]
        public string Copyright { get; set; }
    }
}
