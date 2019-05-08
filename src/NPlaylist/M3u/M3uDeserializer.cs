using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NPlaylist.M3u.Exceptions;

namespace NPlaylist.M3u
{
    public class M3uDeserializer : IPlaylistDeserializer<M3uPlaylist>
    {
        private const string decimalPattern = @"\d+(?:\.\d+)?";
        private const string titlePattern = @"(?:,(.+))";
        private const string pathPattern = ".+";
        private const string newlinePattern = @"\r?\n";

        /*
         * Groups:
         *  1. Decimal duration
         *  2. Optional Title
         *  3. Path       
        */
        private static readonly Regex mediaGroupingRgx = new Regex(
              $"^#EXTINF:({decimalPattern}){titlePattern}?{newlinePattern}"
            + $"({pathPattern})");

        public M3uPlaylist Deserialize(string input)
        {
            ValidateInput(input);
            input = PreprocessInput(input);

            var playlist = new M3uPlaylist();
            AddMediaItems(playlist, ExtractItems(input));

            PostValidateInput(input, playlist);
            return playlist;
        }

        private void ValidateInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!InputStartsWithM3uHeader(input))
            {
                throw new FormatException();
            }
        }

        /*
         * To ease the implementation, the input must have a trailing newline.
        */
        private string PreprocessInput(string input)
        {
            return StringUtils.RemoveExtraNewlines(input + '\n');
        }

        /*
         * Break the input into groups of 2 lines.
         * Extract the media items from these groups.
        */
        private IEnumerable<M3uItem> ExtractItems(string input)
        {
            var twoLinesPattern = $".*{newlinePattern}.*{newlinePattern}";
            foreach (var match in Regex.Matches(StringUtils.RemoveTheFirstLine(input), twoLinesPattern))
            {
                yield return DeserializeMedia(match.ToString());
            }
        }

        private void AddMediaItems(M3uPlaylist playlist, IEnumerable<M3uItem> items)
        {
            foreach (var item in items)
            {
                playlist.Add(item);
            }
        }

        private void PostValidateInput(string input, M3uPlaylist playlist)
        {
            if (!HasTheExpectedNbOfLines(input, playlist.GenericItems.Count()))
            {
                throw new FormatException("Unexpected number of lines.");
            }
        }

        private bool InputStartsWithM3uHeader(string input)
        {
            return Regex.IsMatch(input, $"^#EXTM3U8?({newlinePattern}|$)");
        }

        private M3uItem DeserializeMedia(string mediaStr)
        {
            var match = mediaGroupingRgx.Match(mediaStr);
            if (!match.Success)
            {
                throw new MediaFormatException();
            }

            var duration = decimal.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
            var title = match.Groups[2].Value.Trim();
            var path = match.Groups[3].Value;

            return new M3uItem(path, duration)
            {
                Title = title
            };
        }

        private bool HasTheExpectedNbOfLines(string input, int nbOfMediaItems)
        {
            const int headerNbOfLines = 1;
            const int mediaNbOfLines = 2;

            var actualNbofLines = Regex.Matches(input, newlinePattern).Count;
            var expectedNbOfLines = headerNbOfLines + (nbOfMediaItems * mediaNbOfLines);

            return expectedNbOfLines == actualNbofLines;
        }
    }
}
