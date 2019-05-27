using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using NPlaylist.Wpl;
using Xunit;

namespace NPlaylist.Tests.Wpl
{
    public class WplSerializerTests
    {
        [Fact]
        public void Serialize_NullInput_ThrowsException()
        {
            var serializer = new WplSerializer();

            Action act = () => serializer.Serialize(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Serialize_EmptyPlaylist_ReturnsEmptyPlaylistStr()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <\?wpl .*\?>
                <smil .*>
                    <head />
                    <body>
                        <seq />
                    </body>
                </smil>
            ");
            var serializer = new WplSerializer();

            var output = serializer.Serialize(new WplPlaylist());

            output.Should().MatchRegex(pattern);
        }

        [Fact]
        public void Serialize_GivenATag_SerializeTheTag()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <head>
                    <meta name=""Foo"" content=""Bar"" />
                </head>
            ");
            var serializer = new WplSerializer();

            var playlist = new WplPlaylist();
            playlist.Tags["Foo"] = "Bar";

            var actual = serializer.Serialize(playlist);

            actual.Should().MatchRegex(pattern);
        }

        [Fact]
        public void Serialize_GivenTitle_SerializeTheTitle()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <head>
                    <title>Foo</title>
                </head>
            ");

            var playlist = new WplPlaylist
            {
                Title = "Foo"
            };
            var serializer = new WplSerializer();

            var actual = serializer.Serialize(playlist);

            actual.Should().MatchRegex(pattern);
        }

        [Fact]
        public void Serialize_GivenAuthor_SerializeTheAuthor()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <head>
                    <author>Foo</author>
                </head>
            ");

            var playlist = new WplPlaylist { Author = "Foo" };
            var serializer = new WplSerializer();

            var actual = serializer.Serialize(playlist);

            actual.Should().MatchRegex(pattern);
        }

        [Fact]
        public void Serialize_GivenAMedia_SerializeTheMedia()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <body>
                    <seq>
                        <media src=""Foo"" />
                    </seq>
                </body>
            ");

            var playlist = new WplPlaylist();
            playlist.Add(new WplItem("Foo"));
            var serializer = new WplSerializer();
            var actual = serializer.Serialize(playlist);

            actual.Should().MatchRegex(pattern);
        }

        [Fact]
        public void Serialize_GivenAMediaWithTrackId_SerializeTheMedia()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <body>
                    <seq>
                        <media src=""Foo"" tid=""Bar"" />
                    </seq>
                </body>
            ");

            var playlist = new WplPlaylist();
            playlist.Add(new WplItem("Foo") { TrackId = "Bar" });
            var serializer = new WplSerializer();
            var actual = serializer.Serialize(playlist);

            actual.Should().MatchRegex(pattern);
        }

        /// <summary>
        /// Replaces spaces, tabs and stuff, so that I can still write
        /// pretty xml in tests.
        /// Replaces any \s before '<' and after '>' with \s*
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string PrepareXmlForPatternMatching(string str)
        {
            str = Regex.Replace(str, @"(\s*)(<)", @"\s*$2");
            str = Regex.Replace(str, @"(>)(\s*)", @"$1\s*");
            return str;
        }
    }
}
