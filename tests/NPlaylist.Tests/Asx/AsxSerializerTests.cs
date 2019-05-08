using System;
using System.Linq;
using NPlaylist.Asx;
using Xunit;

namespace NPlaylist.Tests.Asx
{
    public class AsxSerializerTests
    {
        private AsxSerializer serializer;

        public AsxSerializerTests()
        {
            serializer = new AsxSerializer();
        }

        [Fact]
        public void Serialize_NullInput_ThrowException()
        {
            var output = serializer.Serialize(null);
            Assert.True(output == string.Empty);
        }

        [Fact]
        public void Serialize_VersionIsParsedAsExpected()
        {
            var playlist = new AsxPlaylist {Version = "1.0"};
            var asxWithVersion = @"<asx version=""1.0"" />";

            var output = serializer.Serialize(playlist);
            Assert.True(output == asxWithVersion);
        }

        [Fact]
        public void Serialize_TitleOnlyIsParsedAsExpected_Foo()
        {
            var playlist = new AsxPlaylist { Title = "Foo" };
            var asxWithTitle = @"<title>Foo</title>";

            var output = serializer.Serialize(playlist);
            Assert.Contains(asxWithTitle, output);
        }

        [Fact]
        public void Serialize_EmptyEntryIsParsed()
        {
            var playlist = new AsxPlaylist();
            playlist.Add(new AsxItem(string.Empty));
            var cleanAsx = @"<asx />";

            var output = serializer.Serialize(playlist);
            Assert.True(output == cleanAsx);
        }

        [Fact]
        public void Serialize_OnlyParamIsParsed_FooToBar()
        {
            var playlist = new AsxPlaylist();
            playlist.Add(new AsxItem(string.Empty));
            var asxItem = playlist.GenericItems.First();
            asxItem.Tags["Foo"] = "Bar";
            
            var asxWithVersion = @"<asx />";

            var output = serializer.Serialize(playlist);
            Assert.True(output == asxWithVersion);
        }
    }
}
