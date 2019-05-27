using System;
using System.Linq;
using FluentAssertions;
using NPlaylist.Asx;
using Xunit;

namespace NPlaylist.Tests.Asx
{
    public class AsxDeserializerTests
    {
        [Fact]
        public void Deserialize_NullInput_ThrowsArgumentException()
        {
            var deserializer = new AsxDeserializer();

            Action act = () => deserializer.Deserialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_EmptyInput_ThrowsArgumentException()
        {
            var deserializer = new AsxDeserializer();

            Action act = () => deserializer.Deserialize(string.Empty);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_IncorrectFormat_ThrowsFormatException()
        {
            var deserializer = new AsxDeserializer();

            Action act = () => deserializer.Deserialize("Foo");

            act.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_TagIsParsedAsExpected()
        {
            var deserializer = new AsxDeserializer();

            var playlist = deserializer.Deserialize(@"
                <asx />
            ");

            playlist.Items.Should().BeEmpty();
        }

        [Fact]
        public void Deserialize_TitleIsParsedAsExpected()
        {
            var deserializer = new AsxDeserializer();

            var playlist = deserializer.Deserialize(@"
                <asx>
                  <title>Foo</title>
                </asx>
            ");

            playlist.Title.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_VersionIsParsedAsExpected()
        {
            var deserializer = new AsxDeserializer();

            var playlist = deserializer.Deserialize(@"
                <asx version=""Foo"">
                </asx>
            ");

            playlist.Version.Should().Be("Foo");
        }

        [Fact]
        public void Deserialize_RefIsParsedAsExpected_HasOneItem()
        {
            var deserializer = new AsxDeserializer();


            var playlist = deserializer.Deserialize(@"
                <asx>
                  <entry>
                    <ref href=""Foo"" />
                  </entry>
                </asx>
            ");

            playlist.Items.Should().HaveCount(1);
        }

        [Fact]
        public void Deserialize_RefIsParsedAsExpected()
        {
            var deserializer = new AsxDeserializer();


            var playlist = deserializer.Deserialize(@"
                <asx>
                  <entry>
                    <ref href=""Foo"" />
                  </entry>
                </asx>
            ");

            var asxItem = playlist.Items.First();

            asxItem.Path.Should().Be("Foo");
        }
    }
}
