using System.Collections.Generic;
using System.Linq;
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
            playlist.GetItems().Returns(new[] { item });

            var sut = new XspfPlaylist(playlist);
            var actualNbOfItems = sut.GetItems().Count();

            Assert.Equal(1, actualNbOfItems);
        }

        [Fact]
        public void Conversion_ConvertedPlaylistContainsTagAuthor_True()
        {
            var tags = new Dictionary<string, string> { { TagNames.Author, "value" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);

            Assert.Contains(TagNames.Author, sut.Tags.Keys);
        }

        [Fact]
        public void Conversion_ConvertedAuthorEqualsToInitialPlaylitTitle_True()
        {
            var tags = new Dictionary<string, string> { { TagNames.Author, "test author" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);
            var actualAuthorTag = sut.Tags[TagNames.Author];

            Assert.Equal("test author", actualAuthorTag);
        }

        [Fact]
        public void Conversion_ConvertedTitleEqualsToInitialPlaylitTitle_True()
        {
            var tags = new Dictionary<string, string> { { TagNames.Title, "test title" } };
            var playlist = Substitute.For<IPlaylist>();
            playlist.Tags.Returns(tags);

            var sut = new XspfPlaylist(playlist);
            var actualTitleTag = sut.Tags[TagNames.Title];

            Assert.Equal("test title", actualTitleTag);
        }

        [Fact]
        public void Conversion_InitialItemLocationEqualsConverted_True()
        {
            var item = Substitute.For<IPlaylistItem>();
            item.Path.Returns("test path");

            var playlist = Substitute.For<IPlaylist>();
            playlist.GetItems().Returns(new[] { item });

            var sut = new XspfPlaylist(playlist);
            var actualPath = sut.GetItems().First().Path;

            Assert.Equal("test path", actualPath);
        }

        [Fact]
        public void Conversion_ItemTagsInConvertedPlaylistContainsTrackId_True()
        {
            var tags = new Dictionary<string, string> { { "foo", "testID" } };
            var item = Substitute.For<IPlaylistItem>();
            item.Tags.Returns(tags);

            var playlist = Substitute.For<IPlaylist>();
            playlist.GetItems().Returns(new[] { item });

            var sut = new XspfPlaylist(playlist);
            var actualTag = sut.GetItems().First().Tags["foo"];

            Assert.Equal("testID", actualTag);
        }
    }
}
