using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NPlaylist.Xspf;
using NSubstitute;
using Xunit;

namespace NPlaylist.Tests.XspfTests
{
    public class XspfConversionTest
    {
        [Fact]
        public void Conversion_CorrectNumberOfItemsAfterConversion_True()
        {
            var playlist = Substitute.For<IPlaylist>();
            var item = Substitute.For<IPlaylistItem>();
            playlist.GetGenericItems().Returns(new[] { item });
            var sut = new XspfPlaylist(playlist);

            var actualNbOfItems = sut.GetGenericItems().Count();

            actualNbOfItems.Should().Be(1);
        }

        [Fact]
        public void Conversion_ConvertedPlaylistContainsTagAuthor_True()
        {
            var tags = new Dictionary<string, string> { { CommonTags.Author, "value" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);

            sut.Tags.Keys.Should().Contain(CommonTags.Author);
        }

        [Fact]
        public void Conversion_ConvertedAuthorEqualsToInitialPlaylitTitle_True()
        {
            var tags = new Dictionary<string, string> { { CommonTags.Author, "test author" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);
            var actualAuthorTag = sut.Tags[CommonTags.Author];

            actualAuthorTag.Should().Be("test author");
        }

        [Fact]
        public void Conversion_ConvertedTitleEqualsToInitialPlaylitTitle_True()
        {
            var tags = new Dictionary<string, string> { { CommonTags.Title, "test title" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);
            var actualTitleTag = sut.Tags[CommonTags.Title];

            actualTitleTag.Should().Be("test title");
        }

        [Fact]
        public void Conversion_InitialItemLocationEqualsConverted_True()
        {
            var item = Substitute.For<IPlaylistItem>();
            item.Path.Returns("test path");
            var playlist = Substitute.For<IPlaylist>();
            playlist.GetGenericItems().Returns(new[] { item });
            var sut = new XspfPlaylist(playlist);

            var actualPath = sut.GetGenericItems().First().Path;

            actualPath.Should().Be("test path");
        }

        [Fact]
        public void Conversion_ItemTagsInConvertedPlaylistContainsTrackId_True()
        {
            var tags = new Dictionary<string, string> { { "foo", "testID" } };
            var item = Substitute.For<IPlaylistItem>();
            item.Tags.Returns(tags);

            var playlist = Substitute.For<IPlaylist>();
            playlist.GetGenericItems().Returns(new[] { item });

            var sut = new XspfPlaylist(playlist);

            var actualTag = sut.GetGenericItems().First().Tags["foo"];

            actualTag.Should().Be("testID");
        }
    }
}
