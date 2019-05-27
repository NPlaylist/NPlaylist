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
            var itemNumber = 0;
            foreach (var item in playlist.Items)
            {
                itemNumber++;

                sb.Append("File").Append(itemNumber).Append('=').AppendLine(item.Path);
                if (item.Title != null)
                {
                    sb.Append("Title").Append(itemNumber).Append('=').AppendLine(item.Title);
                }

                if (item.Length != null)
                {
                    var result = int.TryParse(item.Length, out var res) ? res : 0;
                    sb.Append("Length").Append(itemNumber).Append('=').Append(result).AppendLine();
                }

                sb.AppendLine();
            }
        }

        private void AddFooter(PlsPlaylist playlist, StringBuilder sb)
        {
            sb.Append("NumberOfEntries=").Append(playlist.Items.Count()).AppendLine();
            var result = int.TryParse(playlist.Version, out var res) ? res : 2;
            sb.Append("Version=").Append(result).AppendLine();
        }

        private void AddHeader(StringBuilder sb)
        {
            sb.AppendLine("[playlist]");
            sb.AppendLine();
        }
    }
}
