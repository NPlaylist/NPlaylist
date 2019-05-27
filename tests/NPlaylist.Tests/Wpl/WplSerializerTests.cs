using System;
using System.Text.RegularExpressions;
using NPlaylist.Wpl;
using Xunit;

namespace NPlaylist.Tests.Wpl
{
    public class WplSerializerTests
    {
        private readonly WplSerializer serializer;

        public WplSerializerTests()
        {
            serializer = new WplSerializer();
        }

        [Fact]
        public void Serialize_NullInput_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => serializer.Serialize(null));
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

            var output = serializer.Serialize(new WplPlaylist());
            Assert.Matches(pattern, output);
        }

        [Fact]
        public void Serialize_GivenATag_SerializeTheTag()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <head>
                    <meta name=""Foo"" content=""Bar"" />
                </head>
            ");

            var playlist = new WplPlaylist();
            playlist.Tags["Foo"] = "Bar";

            Assert.Matches(pattern, serializer.Serialize(playlist));
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

            Assert.Matches(pattern, serializer.Serialize(playlist));
        }

        [Fact]
        public void Serialize_GivenAuthor_SerializeTheAuthor()
        {
            var pattern = PrepareXmlForPatternMatching(@"
                <head>
                    <author>Foo</author>
                </head>
            ");

            var playlist = new WplPlaylist
            {
                Author = "Foo"
            };

            Assert.Matches(pattern, serializer.Serialize(playlist));
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

            Assert.Matches(pattern, serializer.Serialize(playlist));
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
            playlist.Add(new WplItem("Foo")
            {
                TrackId = "Bar"
            });

            Assert.Matches(pattern, serializer.Serialize(playlist));
        }

        /*
         * Replaces spaces, tabs and stuff, so that I can still write
         * pretty xml in tests.
         * Replaces any \s before '<' and after '>' whith \s*
        */

        private static string PrepareXmlForPatternMatching(string str)
        {
            str = Regex.Replace(str, @"(\s*)(<)", @"\s*$2");
            str = Regex.Replace(str, @"(>)(\s*)", @"$1\s*");
            return str;
        }
    }
}
