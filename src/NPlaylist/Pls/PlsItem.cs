using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPlaylist.Pls
{
    public class PlsItem : BasePlaylistItem
    {
        public PlsItem(string path) : base(path)
        {
        }

        public PlsItem(IPlaylistItem item) : base(path: item.Path)
        {
        }

        public string Title
        {
            get => Tags.TryGetValue(TagNames.Title, out var value) ? value : null;
            set => Tags[TagNames.Title] = value;
        }

        public string Length
        {
            get => Tags.TryGetValue(TagNames.Length, out var value) ? value : null;
            set => Tags[TagNames.Length] = value;
        }
    }
}
