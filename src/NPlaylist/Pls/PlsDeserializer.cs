using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPlaylist.Pls.Helper;

namespace NPlaylist.Pls
{
    public class PlsDeserializer : IPlaylistDeserializer<PlsPlaylist>
    {
        private const string locationPattern = @".+";
        private const string titlePattern = @".+";
        private const string lengthPattern = @"-?\d+";
        private const string versionPattern = @"Version=(\d)";

        private static readonly Regex entryGroupRegex = new Regex(
              $@"File\d+=({locationPattern})\n?"
            + $@"(?:Title\d+=({titlePattern})\n?)?"
            + $@"(?:Length\d+=({lengthPattern})\n?)?");

        public PlsPlaylist Deserialize(string input)
        {
            ValidateInputFormat(input);

            var playlist = new PlsPlaylist();

            AddPlaylistVersion(playlist, input);
            AddPlaylistItems(playlist, input);

            return playlist;
        }

        private void AddPlaylistVersion(PlsPlaylist playlist, string input)
        {
            var match = Regex.Match(input, versionPattern).Groups[1].Value;

            playlist.Version = match;
        }

        private void AddPlaylistItems(PlsPlaylist playlist, string input)
        {
            input = StringUtils.RemoveMetaLines(input);

            foreach (var item in ExtractItems(input))
            {
                playlist.Add(item);
            }
        }

        private IEnumerable<PlsItem> ExtractItems(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                yield break;
            }

            var lines = Regex.Split(StringUtils.RemoveFileUnusedLines(input), @"\n\n");

            foreach (var line in lines)
            {
                yield return GetOneItem(line);
            }
        }

        private PlsItem GetOneItem(string block)
        {
            var match = entryGroupRegex.Match(StringUtils.RemoveExtraNewLines(block));
            if (!match.Success)
            {
                throw new FormatException();
            }

            var path = match.Groups[1].Value;
            var title = match.Groups[2].Value;
            var length = match.Groups[3].Value;

            return new PlsItem(path)
            {
                Title = title,
                Length = length
            };
        }

        private void ValidateInputFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!StringUtils.InputHeaderValidation(input))
            {
                throw new FormatException("Missing format header");
            }
        }
    }
}
