using System.Collections.Generic;

namespace NPlaylist
{
    public abstract class BasePlaylistItem : IPlaylistItem
    {
        public IDictionary<string, string> Tags { get; }

        public string Path
        {
            get => Tags.TryGetValue(CommonTags.Path, out var value) ? value : null;
            set => Tags[CommonTags.Path] = value;
        }

        protected BasePlaylistItem(string path)
        {
            Tags = new Dictionary<string, string>();
            Path = path;
        }

        protected BasePlaylistItem(IPlaylistItem item) : this(item.Path)
        {
            foreach (var itemTag in item.Tags)
            {
                Tags[itemTag.Key] = itemTag.Value;
            }
        }
    }
}
