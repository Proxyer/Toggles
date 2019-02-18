using System.Text.RegularExpressions;

namespace Toggles
{
    // Utility class for string handling.
    internal static class StringUtil
    {
        static Regex regex = new Regex("[^a-zA-Z0-9 -]");

        // Conforms strings to game/mod limitations, e g XML tags.
        internal static string Conform(string input)
        {
            // Remove all characters that are not numbers or letters
            string str = regex.Replace(input, "");
            // Uppercase all letters at start of word
            str = Regex.Replace(str, @"\b\w", (Match match) => match.ToString().ToUpper());
            return str.Replace(" ", string.Empty);
        }

        // Attempts to make auto-generated strings pretty-ish.
        internal static string Pretty(string input)
        {
            string str = input;
            //if (str.EndsWith("Entry"))
            //    str.Replace("Entry", "");
            //if (str.EndsWith("Play"))
            //    str.Replace("Play", "");

            str = str.Substring(str.IndexOf("_") + 1);

            // Puts space between letter and capital letter.
            str = Regex.Replace(str, "([a-z])([A-Z])", "$1 $2");
            str = str.Replace("_", ": ");
            return str;
        }
    }
}