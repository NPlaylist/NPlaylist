using System;
using System.Linq;
using FluentAssertions;
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
            Action action = () => xspfDeserializer.Deserialize("test string");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_CorrectVersionParsing_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            var correctVersionTest = "<playlist version = \"1\"  xmlns=\"http://xspf.org/ns/0/\" >  <trackList></trackList></playlist>";

            var obj = xspfDeserializer.Deserialize(correctVersionTest);

            obj.Version.Should().Be("1");
        }

        [Fact]
        public void Deserialize_CorectNumberOfItems_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            var correctCountOfItems = "<?xml version=\"1.0\" encoding=\"utf-8\"?><playlist  xmlns=\"http://xspf.org/ns/0/\" > <trackList><track><title>Windows Path</title>     <location>file:///C:/music/foo.mp3</location>    </track><track><title>Linux Path</title><location>file:///media/music/foo.mp3</location>   </track>   </trackList></playlist>";

            var obj = xspfDeserializer.Deserialize(correctCountOfItems);

            obj.Items.Should().HaveCount(2);
        }

        [Fact]
        public void Deserialize_ItemParsedAsExpected_True()
        {
            var xspfDeserializer = new XspfDeserializer();
            var correctItemParsing = "<?xml version=\"1.0\" encoding=\"utf-8\"?><playlist  xmlns=\"http://xspf.org/ns/0/\" > <trackList><track><title>Linux Path</title><location>file:///media/music/foo.mp3</location>   </track>   </trackList></playlist>";
            var obj = xspfDeserializer.Deserialize(correctItemParsing);

            obj.Items.SingleOrDefault().Title.Should().Be("Linux Path");
        }

        [Fact]
        public void Deserialize_NullInputAsParameter_ArgumentNullExceptionThrown()
        {
            var xspfDeserializer = new XspfDeserializer();
            Action action = () => xspfDeserializer.Deserialize(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
