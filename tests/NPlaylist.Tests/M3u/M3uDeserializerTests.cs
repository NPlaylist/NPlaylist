using System;
using System.Linq;
using FluentAssertions;
using NPlaylist.M3u;
using NPlaylist.M3u.Exceptions;
using Xunit;

namespace NPlaylist.Tests.M3u
{
    public class M3uDeserializerTests
    {
        [Fact]
        public void Deserialize_NullInput_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_InvalidHeader_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#M3uFoo\n");

            act.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_GivenEmptyPlaylist_ReturnsEmptyPlaylist()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U");

            output.Items.Should().BeEmpty();
        }

        [Fact]
        public void Deserialize_TrashMediaTag_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#EXTM3U\n#Foo:42\nfoo.bar\n");

            act.Should().Throw<MediaFormatException>();
        }

        [Theory]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void Deserialize_DifferentTypesOfNewLines_ParseAsExpected(string newLine)
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize($"#EXTM3U{newLine}#EXTINF:42.42{newLine}foo.bar{newLine}");

            output.Items.Should().NotBeEmpty();
        }

        [Fact]
        public void Deserialize_MediaWithNoPath_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#EXTM3U\n#Foo:42\n");

            act.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_MediaWithNoDuration_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#EXTM3U\n#EXTINF:\nfoo.bar\n");

            act.Should().Throw<MediaFormatException>();
        }

        [Fact]
        public void Deserialize_MediaWithInvalidDuration_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#EXTM3U\n#EXTINF:42.foo\nfoo.bar\n");

            act.Should().Throw<MediaFormatException>();
        }

        [Fact]
        public void Deserialize_MediaWithValidDuration_DurationIsCorrectlyExtracted()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n#EXTINF:42.42\nfoo.bar\n");

            output.Items.First().Duration.Should().Be(42.42m);
        }

        [Fact]
        public void Deserialize_MediaWithMissingTitle_ThrowsException()
        {
            var deserializer = new M3uDeserializer();

            Action act = () => deserializer.Deserialize("#EXTM3U\n#EXTINF:42,\nfoo.bar\n");

            act.Should().Throw<MediaFormatException>();
        }

        [Fact]
        public void Deserialize_MediaWithTitle_TitleIsCorrectlyExtracted()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n#EXTINF:42, Foo\nfoo.bar\n");

            output.Items.First().Title.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_MediaWithPath_PathIsCorrectlyExtracted()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n#EXTINF:42\nfoo.bar\n");

            output.Items.First().Path.Should().Be("foo.bar");
        }

        [Fact]
        public void Deserialize_MediaWithTwoItems_TwoItemsAreExtracted()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n#EXTINF:42\nfoo.bar\n#EXTINF:42\nfoo.bar\n");

            output.Items.Should().HaveCount(2);
        }

        [Fact]
        public void Deserialize_MediaAndUnnecessaryNewlines_ExtraNewlinesAreIgnored()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n\n\n\n\n\n\n#EXTINF:42\nfoo.bar\n");

            output.Items.Should().NotBeEmpty();
        }

        [Fact]
        public void Deserialize_MeidaTitleContainsComma_TitleExtracteWithComma()
        {
            var deserializer = new M3uDeserializer();

            var output = deserializer.Deserialize("#EXTM3U\n#EXTINF:42, Foo, Bar\nfoo.bar\n");

            output.Items.First().Title.Should().Be("Foo, Bar");
        }
    }
}
