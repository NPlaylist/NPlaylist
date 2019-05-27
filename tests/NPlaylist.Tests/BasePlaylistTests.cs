using NSubstitute;
using Xunit;

namespace NPlaylist.Tests
{
    public class BasePlaylistTests
    {
        private readonly BasePlaylist<IPlaylistItem> playlist;
        private readonly IPlaylistItem dummyItem;

        public BasePlaylistTests()
        {
            playlist = Substitute.For<BasePlaylist<IPlaylistItem>>();
            dummyItem = Substitute.For<IPlaylistItem>();
        }

        [Fact]
        public void Add_Adds_ExpectedItem()
        {
            playlist.Add(dummyItem);

            Assert.Contains(dummyItem, playlist.GetGenericItems());
        }

        [Fact]
        public void Remove_Removes_ExpectedItem()
        {
            playlist.Add(dummyItem);

            playlist.Remove(dummyItem);

            Assert.DoesNotContain(dummyItem, playlist.GetGenericItems());
        }
    }
}
