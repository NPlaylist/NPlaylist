using System.Collections.Generic;

namespace NPlaylist.Xspf
{
    public class XspfPlaylistItem : BasePlaylistItem
    {
        public string Title
        {
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        public XspfPlaylistItem(string path) : base(path)
        {
        }

        public XspfPlaylistItem(IPlaylistItem item) : base(item.Path)
        {
            foreach (var itemTag in item.Tags)
            {
                Tags[itemTag.Key] = itemTag.Value;
            }
        }
    }
}
