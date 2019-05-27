using System;
using FluentAssertions;
using NPlaylist.M3u;
using Xunit;

namespace NPlaylist.Tests.M3u
{
    public class M3uSerializerTests
    {
        private const string newlinePattern = @"\r?\n";

        [Fact]
        public void Serialize_NullInput_ThrowsException()
        {
            var serializer = new M3uSerializer();

            Action act = () => serializer.Serialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Serialize_EmptyPlaylist_ReturnsEmptyPlaylistStr()
        {
            var serializer = new M3uSerializer();

            var output = serializer.Serialize(new M3uPlaylist());

            output.Should().MatchRegex("^#EXTM3U8?");
        }

        [Fact]
        public void Serialize_HasAMedia_MediaIsCorrectlyFormated()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 42.42m));
            var serializer = new M3uSerializer();

            var output = serializer.Serialize(playlist);

            output.Should().MatchRegex($"#EXTINF:42.42{newlinePattern}foo{newlinePattern}");
        }

        [Fact]
        public void Serialize_HasAMediaWithWhitespaceTitle_TitleIsIgnored()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 0) { Title = " \t" });
            var serializer = new M3uSerializer();

            var output = serializer.Serialize(playlist);

            output.Should().MatchRegex($"#EXTINF:0{newlinePattern}");
        }

        [Fact]
        public void Serialize_HasAMediaWithNormalTitle_TitleIsTrimmedAndAdded()
        {
            var playlist = new M3uPlaylist();
            playlist.Add(new M3uItem("foo", 0) { Title = "\t Foo \t" });
            var serializer = new M3uSerializer();

            var output = serializer.Serialize(playlist);

            output.Should().MatchRegex($"#EXTINF:0, Foo{newlinePattern}");
        }
    }
}
