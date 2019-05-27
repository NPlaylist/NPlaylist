using System;
using System.Linq;
using FluentAssertions;
using NPlaylist.Wpl;
using Xunit;

namespace NPlaylist.Tests.Wpl
{
    public class WplDeserializerTests
    {
        [Fact]
        public void Deserialize_NullInput_ThrowsArgumentNullException()
        {
            var deserializer = new WplDeserializer();

            Action act = () => deserializer.Deserialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_EmptyInput_ThrowsArgumentNullException()
        {
            var deserializer = new WplDeserializer();

            Action act = () => deserializer.Deserialize(string.Empty);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_IncorrectFormat_ThrowsFormatException()
        {
            var deserializer = new WplDeserializer();

            Action act = () => deserializer.Deserialize("Foo");

            act.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_GivenATag_HasExpectedTag()
        {
            const string serializedPlaylist =
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
            var deserializer = new WplDeserializer();

            var playlist = deserializer.Deserialize(serializedPlaylist);

            playlist.Tags["Foo"].Should().Be("Bar");
        }

        [Fact]
        public void Deserialize_GivenTitle_HasExpectedTitle()
        {
            const string serializedPlaylist =
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
            var deserializer = new WplDeserializer();

            var playlist = deserializer.Deserialize(serializedPlaylist);

            playlist.Title.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_GivenAuthor_HasExpectedAuthor()
        {
            const string serializedPlaylist =
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
            var deserializer = new WplDeserializer();

            var playlist = deserializer.Deserialize(serializedPlaylist);

            playlist.Author.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_GivenAMedia_HasExpectedPath()
        {
            const string serializedPlaylist =
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
            var deserializer = new WplDeserializer();

            var playlist = deserializer.Deserialize(serializedPlaylist);

            var item = playlist.Items.First();

            item.Path.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_GivenAMedia_HasExpectedTrackID()
        {
            const string serializedPlaylist =
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
            var deserializer = new WplDeserializer();

            var playlist = deserializer.Deserialize(serializedPlaylist);

            var item = playlist.Items.First();

            item.TrackId.Should().Be("Bar");
        }
    }
}
