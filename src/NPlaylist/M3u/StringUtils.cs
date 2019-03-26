using System;
using System.Text.RegularExpressions;

namespace NPlaylist.M3u
{
    internal static class StringUtils
    {
        public static string RemoveExtraNewlines(string str)
        {
            return Regex.Replace(str, "\n\n+", "\n");
        }

        public static string RemoveTheFirstLine(string input)
        {
            return Regex.Replace(input, "^.*\n", "");
        }
    }
}
