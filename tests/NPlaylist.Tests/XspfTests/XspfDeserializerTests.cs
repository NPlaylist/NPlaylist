using System;
using System.Linq;
using NPlaylist.Xspf;
using Xunit;

namespace NPlaylist.Tests
{
    public class XspfDeserializerTests
    {
        [Fact]
        public void Deserialize_InvalidPlaylistType_InvalidOperationException()
        {
            var xspfDeserializer = new XspfDeserializer();

            Assert.Throws<FormatException>(() => xspfDeserializer.Deserialize("test string"));
        }

        [Fact]
        public void Deserialize_CorrectVersionParsing_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            string correctVersionTest =
                "<playlist version = \"1\"  xmlns=\"http://xspf.org/ns/0/\" >  <trackList></trackList></playlist>";

            var obj = xspfDeserializer.Deserialize(correctVersionTest);
            Assert.True(obj.Version == "1");
        }

        [Fact]
        public void Deserialize_CorectNumberOfItems_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            string correctCountOfItems =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<playlist  xmlns=\"http://xspf.org/ns/0/\" > " +
                "<trackList>" +
                "<track>" +
                "<title>Windows Path</title>     " +
                "<location>file:///C:/music/foo.mp3</location>    " +
                "</track>" +
                "<track>" +
                "<title>Linux Path</title>" +
                "<location>file:///media/music/foo.mp3</location>   " +
                "</track>   " +
                "</trackList>" +
                "</playlist>";

            var obj = xspfDeserializer.Deserialize(correctCountOfItems);
            Assert.True(obj.GenericItems.Count() == 2);
        }

        [Fact]
        public void Deserialize_ItemParsedAsExpected_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            string correctItemParsing =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<playlist  xmlns=\"http://xspf.org/ns/0/\" > " +
                "<trackList>" +
                "<track>" +
                "<title>Linux Path</title>" +
                "<location>file:///media/music/foo.mp3</location>   " +
                "</track>   " +
                "</trackList>" +
                "</playlist>";
            var obj = xspfDeserializer.Deserialize(correctItemParsing);
            
            Assert.True(obj.GenericItems.SingleOrDefault().Title == "Linux Path");
        }

        [Fact]
        public void Deserialize_NullInputAsParameter_ArgumentNullExceptionThrown()
        {
            var xspfDeserializer = new XspfDeserializer();
           
            Assert.Throws<ArgumentNullException>(()=> xspfDeserializer.Deserialize(null));
        }
    }
}
