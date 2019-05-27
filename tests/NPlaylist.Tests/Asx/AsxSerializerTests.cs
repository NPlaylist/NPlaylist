using System.Linq;
using FluentAssertions;
using NPlaylist.Asx;
using Xunit;

namespace NPlaylist.Tests.Asx
{
    public class AsxSerializerTests
    {
        [Fact]
        public void Serialize_NullInput_ThrowException()
        {
            var serializer = new AsxSerializer();

            var output = serializer.Serialize(null);

            output.Should().BeEmpty();
        }

        [Fact]
        public void Serialize_VersionIsParsedAsExpected()
        {
            var playlist = new AsxPlaylist { Version = "1.0" };
            var serializer = new AsxSerializer();

            var output = serializer.Serialize(playlist);

            output.Should().Be(@"<asx version=""1.0"" />");
        }

        [Fact]
        public void Serialize_TitleOnlyIsParsedAsExpected_Foo()
        {
            var playlist = new AsxPlaylist { Title = "Foo" };
            var serializer = new AsxSerializer();

            var output = serializer.Serialize(playlist);

            output.Should().Contain("<title>Foo</title>");
        }

        [Fact]
        public void Serialize_EmptyEntryIsParsed()
        {
            var playlist = new AsxPlaylist();
            playlist.Add(new AsxItem(string.Empty));
            var serializer = new AsxSerializer();

            var actual = serializer.Serialize(playlist);

            actual.Should().Be("<asx />");
        }

        [Fact]
        public void Serialize_OnlyParamIsParsed_FooToBar()
        {
            var playlist = new AsxPlaylist();
            playlist.Add(new AsxItem(string.Empty));
            var asxItem = playlist.Items.First();
            asxItem.Tags["Foo"] = "Bar";
            var serializer = new AsxSerializer();

            var actual = serializer.Serialize(playlist);

            actual.Should().Be("<asx />");
        }
    }
}
