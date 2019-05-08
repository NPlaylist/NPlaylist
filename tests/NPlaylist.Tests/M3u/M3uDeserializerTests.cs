using System;
using System.Linq;
using NPlaylist.M3u;
using NPlaylist.M3u.Exceptions;
using Xunit;

namespace NPlaylist.Tests.M3u
{
    public class M3uDeserializerTests
    {
        private readonly M3uDeserializer deserializer;

        public M3uDeserializerTests()
        {
            deserializer = new M3uDeserializer();
        }

        [Fact]
        public void Deserialize_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(null));
        }

        [Fact]
        public void Deserialize_InvalidHeader_ThrowsException()
        {
            Assert.Throws<FormatException>(() => deserializer.Deserialize("#M3uFoo\n"));
        }

        [Fact]
        public void Deserialize_GivenEmptyPlaylist_ReturnsEmptyPlaylist()
        {
            var emptyPlaylistStr = "#EXTM3U";

            var output = deserializer.Deserialize(emptyPlaylistStr);
            Assert.Empty(output.GenericItems);
        }

        [Fact]
        public void Deserialize_TrashMediaTag_ThrowsException()
        {
            var str =
                  "#EXTM3U\n"
                + "#Foo:42\n"
                + "foo.bar\n";

            Assert.Throws<MediaFormatException>(() => deserializer.Deserialize(str));
        }

        [Theory]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void Deserialize_DifferentTypesOfNewLines_ParseAsExpected(string newLine)
        {
            var str =
                  "#EXTM3U" + newLine
                + "#EXTINF:42.42" + newLine
                + "foo.bar" + newLine;

            var output = deserializer.Deserialize(str);
            Assert.NotEmpty(output.GenericItems);
        }

        [Fact]
        public void Deserialize_MediaWithNoPath_ThrowsException()
        {
            var str =
                  "#EXTM3U\n"
                + "#Foo:42\n";

            Assert.Throws<FormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_MediaWithNoDuration_ThrowsException()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:\n"
                + "foo.bar\n";

            Assert.Throws<MediaFormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_MediaWithInvalidDuration_ThrowsException()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42.foo\n"
                + "foo.bar\n";

            Assert.Throws<MediaFormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_MediaWithValidDuration_DurationIsCorrectlyExtracted()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42.42\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.Equal(42.42m, output.GenericItems.First().Duration);
        }

        [Fact]
        public void Deserialize_MediaWithMissingTitle_ThrowsException()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42,\n"
                + "foo.bar\n";

            Assert.Throws<MediaFormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_MediaWithTitle_TitleIsCorrectlyExtracted()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42, Foo\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.Equal("Foo", output.GenericItems.First().Title);
        }

        [Fact]
        public void Deserialize_MediaWithPath_PathIsCorrectlyExtracted()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.Equal("foo.bar", output.GenericItems.First().Path);
        }

        [Fact]
        public void Deserialize_MediaWithTwoItems_TwoItemsAreExtracted()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42\n"
                + "foo.bar\n"
                + "#EXTINF:42\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.Equal(2, output.GenericItems.Count());
        }

        [Fact]
        public void Deserialize_MediaAndUnnecessaryNewlines_ExtraNewlinesAreIgnored()
        {
            var str =
                  "#EXTM3U\n\n\n\n\n\n\n"
                + "#EXTINF:42\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.NotEmpty(output.GenericItems);
        }

        [Fact]
        public void Deserialize_MeidaTitleContainsComma_TitleExtracteWithComma()
        {
            var str =
                  "#EXTM3U\n"
                + "#EXTINF:42, Foo, Bar\n"
                + "foo.bar\n";

            var output = deserializer.Deserialize(str);
            Assert.Equal("Foo, Bar", output.GenericItems.First().Title);
        }
    }
}
