using System;
using System.Linq;
using System.Text;

namespace NPlaylist.Pls
{
    public class PlsSerializer : IPlaylistSerializer<PlsPlaylist>
    {
        public string Serialize(PlsPlaylist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }
            var sb = new StringBuilder();
           
            AddHeader(sb);
            AddBody(playlist, sb);
            AddFooter(playlist, sb);

            return sb.ToString();
        }

        private void AddBody(PlsPlaylist playlist, StringBuilder sb)
        {
            int itemNumber = 0;
            foreach (var item in playlist.GenericItems)
            {
                itemNumber++;

                sb.AppendLine($"File{itemNumber}={item.Path}");
                if (item.Title != null)
                {
                    sb.AppendLine($"Title{itemNumber}={item.Title}");
                }

                if (item.Length != null)
                {
                    var result = int.TryParse(item.Length, out int res) ? res : 0;
                    sb.AppendLine($"Length{itemNumber}={result}");
                }

                sb.AppendLine();
            }
        }

        private void AddFooter(PlsPlaylist playlist, StringBuilder sb)
        {
            sb.AppendLine($"NumberOfEntries={playlist.GenericItems.Count()}");
            var result = int.TryParse(playlist.Version, out int res) ? res : 2;
            sb.AppendLine($"Version={result}");
        }

        private void AddHeader(StringBuilder sb)
        {
            sb.AppendLine("[playlist]");
            sb.AppendLine();
        }
    }
}
