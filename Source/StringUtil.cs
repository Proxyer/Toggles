using System.Text.RegularExpressions;

namespace Toggles
{
    // Utility class for string handling.
    internal static class StringUtil
    {
        static Regex regex = new Regex("[^a-zA-Z0-9 -]");

        // Conforms strings to game/mod limitations, e g XML tags.
        internal static string Sanitize(string input)
        {
            // Remove all characters that are not numbers or letters
            string str = regex.Replace(input, "");
            // Uppercase all letters at start of word
            str = Regex.Replace(str, @"\b\w", (Match match) => match.ToString().ToUpper());
            return str.Replace(" ", string.Empty);
        }

        // Attempts string beautification.
        internal static string Pretty(string str)
        {
            str = str.Substring(str.IndexOf("_") + 1);

            // Puts space between letter and capital letter.
            str = Regex.Replace(str, "([a-z])([A-Z])", "$1 $2");
            str = str.Replace("_", ": ");
            if (str.Length >= 28)
            {
                str = str.Substring(0, 27);
                str += "...";
            }
            return str;
        }
    }
}