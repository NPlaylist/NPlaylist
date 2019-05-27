using System;
using System.Linq;
using FluentAssertions;
using NPlaylist.Pls;
using Xunit;

namespace NPlaylist.Tests.Pls
{
    public class PlsDeserializerTests
    {
        [Fact]
        public void Deserialize_NullInput_ThrowsArgumentNullException()
        {
            var deserializer = new PlsDeserializer();

            Action act = () => deserializer.Deserialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_EmptyInput_ThrowsArgumentException()
        {
            var deserializer = new PlsDeserializer();

            Action act = () => deserializer.Deserialize(string.Empty);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Deserialize_IncorrectFormat_ThrowsFormatException()
        {
            var deserializer = new PlsDeserializer();

            Action act = () => deserializer.Deserialize("Foo");

            act.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_OnlyHeaderIsParsedAsExpected()
        {
            var deserializer = new PlsDeserializer();

            var playlist = deserializer.Deserialize("[playlist]");

            playlist.Items.Should().BeEmpty();
        }

        [Fact]
        public void Deserialize_OnlyHeaderIsParsedExpectingEmptyVersion()
        {
            var deserializer = new PlsDeserializer();

            var playlist = deserializer.Deserialize("[playlist]");

            playlist.Version.Should().BeEmpty();
        }

        [Fact]
        public void Deserialize_VersionIsParsedAsNonDigitNumber()
        {
            var deserializer = new PlsDeserializer();

            var playlist = deserializer.Deserialize("[playlist]\nVersion=Foo");

            playlist.Version.Should().BeEmpty();
        }

        [Fact]
        public void Deserialize_EntryIsParsedWithInvalidFormat()
        {
            var deserializer = new PlsDeserializer();

            Action action = () => deserializer.Deserialize("File1=Foo");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_EntryIsParsedAsExpected_Foo()
        {
            var deserializer = new PlsDeserializer();

            var playlist = deserializer.Deserialize("[playlist]\n\nFile1=Foo");

            playlist.GetGenericItems().Should().HaveCount(1);
        }

        [Fact]
        public void Deserialize_EntryHasTrashTag_FooToBar()
        {
            var deserializer = new PlsDeserializer();

            Action action = () => deserializer.Deserialize("[playlist]\nFoo=Bar");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void Deserialize_EntryWithStreamDuration_CorrectExtracted()
        {
            var deserializer = new PlsDeserializer();

            var playlist = deserializer.Deserialize("[playlist]\nFile1=Foo\nLength1=-1");

            playlist.Items.First().Length.Should().Be("-1");
        }

        [Fact]
        public void Deserialize_EntryWithUselessNewlines_ExtralinesAreIgnored()
        {
            var str = string.Format(
                "[playlist]{0}{1}{2}File1=Foo{3}{4}",
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine);

            var deserializer = new PlsDeserializer();
            var playlist = deserializer.Deserialize(str);

            playlist.Items.First().Path.Should().Be("Foo");
        }
    }
}
