using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NPlaylist.Asx;
using NSubstitute;
using Xunit;

namespace NPlaylist.Tests.Asx
{
    public class AsxConversionTests
    {
        [Fact]
        public void Conversion_CorrespondWithTheSameAmountOfItems_True()
        {
            var playlist = Substitute.For<IPlaylist>();
            var item = Substitute.For<IPlaylistItem>();
            playlist.GetGenericItems().Returns(new[] { item });

            var asx = new AsxPlaylist(playlist);

            asx.GetGenericItems().Should().HaveCount(playlist.GetGenericItems().Count());
        }

        [Fact]
        public void Conversion_ConvertedPlaylistContainsTitleTag_True()
        {
            var playlist = Substitute.For<IPlaylist>();
            var dictionary = new Dictionary<string, string>
            {
                { CommonTags.Author, "Foo" }
            };
            playlist.Tags.Returns(dictionary);

            var asx = new AsxPlaylist(playlist);

            asx.Tags.Should().ContainKey(CommonTags.Author);
        }

        [Fact]
        public void Conversion_ConvertedPlaylistContainsInitialTitle_Foo()
        {
            var playlist = Substitute.For<IPlaylist>();
            var dictionary = new Dictionary<string, string>
            {
                { CommonTags.Author, "Foo" }
            };
            playlist.Tags.Returns(dictionary);

            var asx = new AsxPlaylist(playlist);

            asx.Tags[CommonTags.Author].Should().Be("Foo");
        }

        [Fact]
        public void Conversion_ConvertedPlaylistItemContainsSamePath_Foo()
        {
            var playlist = Substitute.For<IPlaylist>();
            var item = Substitute.For<IPlaylistItem>();
            item.Path.Returns("Foo");
            playlist.GetGenericItems().Returns(new[] { item });

            var asx = new AsxPlaylist(playlist);

            asx.GetGenericItems().First().Path.Should().Be("Foo");
        }

        [Fact]
        public void Conversion_ConvertedPlaylistItemContainsParam_FooToBar()
        {
            var playlist = Substitute.For<IPlaylist>();
            var item = Substitute.For<IPlaylistItem>();
            var dictionary = new Dictionary<string, string>
            {
                { "Foo", "Bar" }
            };
            item.Tags.Returns(dictionary);
            playlist.GetGenericItems().Returns(new[] { item });

            var asx = new AsxPlaylist(playlist);
            asx.GetGenericItems().First().Tags["Foo"].Should().Be("Bar");
        }
    }
}
