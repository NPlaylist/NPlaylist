using System;
using NPlaylist.M3u;
using Xunit;

namespace NPlaylist.Tests.M3u
{
    public class M3uSerializerTests
    {
        private const string newlinePattern = @"\r?\n";

        private readonly M3uSerializer serializer;

        public M3uSerializerTests()
        {
            serializer = new M3uSerializer();
        }

        [Fact]
        public void Serialize_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => serializer.Serialize(null));
        }

        [Fact]
        public void Serialize_EmptyPlaylist_ReturnsEmptyPlaylistStr()
        {
            var output = serializer.Serialize(new M3uPlaylist());
            Assert.Matches(@"^#EXTM3U8?", output);
        }

        [Fact]
        public void Serialize_HasAMedia_MediaIsCorrectlyFormated()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 42.42m));

            var output = serializer.Serialize(playlist);

            var pattern =
                  @"#EXTINF:42.42" + newlinePattern
                + @"foo" + newlinePattern;

            Assert.Matches(pattern, output);
        }

        [Fact]
        public void Serialize_HasAMediaWithWhitespaceTitle_TitleIsIgnored()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 0)
            {
                Title = " \t"
            });

            var output = serializer.Serialize(playlist);

            var pattern = @"#EXTINF:0" + newlinePattern;
            Assert.Matches(pattern, output);
        }

        [Fact]
        public void Serialize_HasAMediaWithNormalTitle_TitleIsTrimmedAndAdded()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 0)
            {
                Title = "\t Foo \t"
            });

            var output = serializer.Serialize(playlist);

            var pattern = @"#EXTINF:0, Foo" + newlinePattern;
            Assert.Matches(pattern, output);
        }
    }
}
