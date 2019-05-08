using System;
using System.Linq;
using NPlaylist.Asx;
using NSubstitute;
using Xunit;

namespace NPlaylist.Tests.Asx
{
    public class AsxDeserializerTests
    {
        readonly AsxDeserializer deserializer;
        
        public AsxDeserializerTests()
        {
            deserializer = new AsxDeserializer();
        }

        [Fact]
        public void Deserialize_NullInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(null));
        }

        [Fact]
        public void Deserialize_EmptyInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(string.Empty));
        }

        [Fact]
        public void Deserialize_IncorrectFormat_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => deserializer.Deserialize("Foo"));
        }

        [Fact]
        public void Deserialize_TagIsParsedAsExpected()
        {
            string asxWithMeta_FooToBar =
            @"
                <asx />
            ";
            var playlist = deserializer.Deserialize(asxWithMeta_FooToBar);
            
            Assert.True(!playlist.GenericItems.Any());
        }

        [Fact]
        public void Deserialize_TitleIsParsedAsExpected()
        {
            string asxWithTitle_Foo =
            @"
                <asx>
                  <title>Foo</title>
                </asx>
            ";
            var playlist = deserializer.Deserialize(asxWithTitle_Foo);
            
            Assert.True(playlist.Title == "Foo");
        }

        [Fact]
        public void Deserialize_VersionIsParsedAsExpected()
        {
            string asxWithVersion_Foo =
            @"
                <asx version=""Foo"">
                </asx>
            ";
            var playlist = deserializer.Deserialize(asxWithVersion_Foo);

            Assert.True(playlist.Version == "Foo");
        }

        [Fact]
        public void Deserialize_RefIsParsedAsExpected()
        {
            string asxWithRef_Foo =
            @"
                <asx>
                  <entry>
                    <ref href=""Foo"" />
                  </entry>
                </asx>
            ";
            var playlist = deserializer.Deserialize(asxWithRef_Foo);

            var asxItem = playlist.GenericItems.First();
            Assert.True(playlist.GenericItems.Count() == 1 && asxItem.Path == "Foo");
        }
    }
}
