using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPlaylist.Asx;
using NPlaylist.Pls;
using Xunit;

namespace NPlaylist.Tests.Pls
{
    public class PlsDeserializerTests
    {
        readonly PlsDeserializer deserializer;

        public PlsDeserializerTests()
        {
            deserializer = new PlsDeserializer();
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
        public void Deserialize_OnlyHeaderIsParsedAsExpected()
        {
            string str = @"[playlist]";
            var playlist = deserializer.Deserialize(str);

            Assert.Empty(playlist.Items);
        }

        [Fact]
        public void Deserialize_OnlyHeaderIsParsedExpectingNullVersion()
        {
            string str = @"[playlist]";
            var playlist = deserializer.Deserialize(str);

            Assert.True(playlist.Version == string.Empty);
        }

        [Fact]
        public void Deserialize_VersionIsParsedAsNonDigitNumber()
        {
            string str = @"[playlist]
Version=Foo";
            var playlist = deserializer.Deserialize(str);

            Assert.True(playlist.Version == string.Empty);
        }

        [Fact]
        public void Deserialize_EntryIsParsedWithInvalidFormat()
        {
            string str = @"File1=Foo";

            Assert.Throws<FormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_EntryIsParsedAsExpected_Foo()
        {
            string str = @"[playlist]

File1=Foo";
            var playlist = deserializer.Deserialize(str);

            Assert.True(playlist.GetItems().Count() == 1);
        }

        [Fact]
        public void Deserialize_EntryHasTrashTag_FooToBar()
        {
            string str = @"[playlist]
Foo=Bar";

            Assert.Throws<FormatException>(() => deserializer.Deserialize(str));
        }

        [Fact]
        public void Deserialize_EntryWithStreamDuration_CorrectExtracted()
        {
            string str = @"[playlist]
File1=Foo
Length1=-1";
            var playlist = deserializer.Deserialize(str);

            Assert.Equal("-1", playlist.Items.First().Length);
        }
    }
}
