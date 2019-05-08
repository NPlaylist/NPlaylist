#pragma warning disable CA2214 // Warns about calls to virtual member functions occuring in the constructor 
#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor

using System.Collections.Generic;
using System.Linq;

namespace NPlaylist
{
    public abstract class BasePlaylist<T> : IPlaylist<T>
        where T : IPlaylistItem
    {
        private readonly List<T> _items;

        protected BasePlaylist()
        {
            Tags = new Dictionary<string, string>();
            _items = new List<T>();
        }

        protected BasePlaylist(IPlaylist playlist) : this()
        {
            foreach (var playlistTag in playlist.Tags)
            {
                Tags[playlistTag.Key] = playlistTag.Value;
            }

            foreach (var item in playlist.GetItems())
            {
                Add(CreateItem(item));
            }
        }

        public IDictionary<string, string> Tags { get; }

        public IEnumerable<T> GenericItems => _items;

        public IEnumerable<IPlaylistItem> GetItems()
        {
            return _items.Cast<IPlaylistItem>();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        protected abstract T CreateItem(IPlaylistItem item);
    }
}
