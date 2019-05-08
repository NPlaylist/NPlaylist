using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NPlaylist.M3u
{
    public class M3uSerializer : IPlaylistSerializer<M3uPlaylist>
    {
        private readonly string _newLine;
        
        public M3uSerializer(string newline = null)
        {
            this._newLine = (newline == null) ? Environment.NewLine : newline;
        }

        public string Serialize(M3uPlaylist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            var sb = new StringBuilder();
            AddHeader(sb);
            AddMediaItems(sb, playlist.GenericItems);

            return sb.ToString();
        }

        private void AddHeader(StringBuilder sb)
        {
            sb.Append("#EXTM3U");
            sb.Append(_newLine);
            sb.Append(_newLine);
        }

        private void AddMediaItems(StringBuilder sb, IEnumerable<M3uItem> mediaItems)
        {
            foreach (var item in mediaItems)
            {
                AddMedia(sb, item);
                sb.Append(_newLine);
            }
        }

        private void AddMedia(StringBuilder sb, M3uItem item)
        {
            AddMediaHeader(sb, item);
            AddMediaBody(sb, item);
        }

        private void AddMediaHeader(StringBuilder sb, M3uItem item)
        {
            string formatedDuration;
            string formatedTitle = null;

            formatedDuration = item.Duration.ToString(CultureInfo.InvariantCulture);

            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                formatedTitle = $", {item.Title}";
            }

            sb.Append("#EXTINF:");
            sb.Append(formatedDuration);
            sb.Append(formatedTitle);

            sb.Append(_newLine);
        }

        private void AddMediaBody(StringBuilder sb, M3uItem item)
        {
            sb.Append(item.Path);
            sb.Append(_newLine);
        }
    }
}
