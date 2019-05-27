using System.Text.RegularExpressions;

namespace NPlaylist.Pls.Helper
{
    internal static class StringUtils
    {
        public static string RemoveFileUnusedLines(string lines)
        {
            return Regex.Replace(lines, "(\r?\n)+File", "\n\nFile");
        }

        public static string RemoveExtraNewLines(string line)
        {
            return Regex.Replace(line, "(\r\n)+", "\n");
        }

        public static string RemoveMetaLines(string lines)
        {
            return Regex.Replace(lines, @"\A(^.*(\r?\n?)+)|((\r?\n?)+(Version|Number).+)|(\r?\n?)+\z", "");
        }

        public static bool InputHeaderValidation(string input)
        {
            return Regex.IsMatch(input, @"^(\[playlist\])(\r|\n|$)?");
        }
    }
}
