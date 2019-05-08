using System;
using System.Linq;
using NPlaylist.Wpl;
using Xunit;

namespace NPlaylist.Tests.Wpl
{
    public class WplDeserializerTests
    {
        private readonly WplDeserializer deserializer;

        public WplDeserializerTests()
        {
            deserializer = new WplDeserializer();
        }

        [Fact]
        public void Deserialize_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(null));
        }

        [Fact]
        public void Deserialize_EmptyInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(string.Empty));
        }

        [Fact]
        public void Deserialize_IncorrectFormat_ThrowsException()
        {
            Assert.Throws<FormatException>(() => deserializer.Deserialize("Foo"));
        }

        [Fact]
        public void Deserialize_GivenATag_TagIsParrsedAsExpected()
        {
            var wplWithMeta_FooToBar =
            @"
                <smil>
                    <head>
                        <meta name=""Foo"" content=""Bar""/>
                    </head>
                    <body>
                        <seq></seq>
                    </body>
                </smil>
            ";

            var playlist = deserializer.Deserialize(wplWithMeta_FooToBar);

            Assert.True(playlist.Tags["Foo"] == "Bar");
        }

        [Fact]
        public void Deserialize_GivenTitle_TitleIsParrsedAsExpected()
        {
            var wplWithTitle_Foo =
            @"
                <smil>
                    <head>
                        <title>Foo</title>
                    </head>
                    <body>
                        <seq></seq>
                    </body>
                </smil>
            ";

            var playlist = deserializer.Deserialize(wplWithTitle_Foo);

            Assert.True(playlist.Title == "Foo");
        }

        [Fact]
        public void Deserialize_GivenAuthor_AuthorIsParrsedAsExpected()
        {
            var wplWithAuthor_Foo =
            @"
                <smil>
                    <head>
                        <author>Foo</author>
                    </head>
                    <body>
                        <seq></seq>
                    </body>
                </smil>
            ";

            var playlist = deserializer.Deserialize(wplWithAuthor_Foo);

            Assert.True(playlist.Author == "Foo");
        }

        [Fact]
        public void Deserialize_GivenAMedia_MediaIsParrsedAsExpected()
        {
            var wplWithMedia_SrcToFoo_TidToBar =
            @"
                <smil>
                    <head></head>
                    <body>
                        <seq>
                            <media src=""Foo"" tid=""Bar""/>
                        </seq>
                    </body>
                </smil>
            ";

            var playlist = deserializer.Deserialize(wplWithMedia_SrcToFoo_TidToBar);

            var wplItem = playlist.GenericItems.First();
            Assert.True(wplItem.Path == "Foo" && wplItem.TrackId == "Bar");
        }
    }
}
