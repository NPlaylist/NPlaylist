using System.Xml.Serialization;

namespace NPlaylist.Asx.AsxParts
{
    [XmlRoot(ElementName = "param")]
    public class ParamItem
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
