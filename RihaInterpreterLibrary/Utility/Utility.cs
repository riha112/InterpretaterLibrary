using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Utility
{
    public static class Utility
    {
        public static bool MatchesRegex(string regex, string value)
        {
            var r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.Match(value).Success;
        }

        public static string RemoveRepeatingSpace(string text)
        {
            var regex = new Regex("[ ]{2,}", RegexOptions.None);
            return regex.Replace(text, " ");
        }

        public static string RemoveColumnSpace(string text)
        {
            var regex = new Regex(":[ ]", RegexOptions.None);
            return regex.Replace(text, ":");
        }

        public static T GetValueType<T>(string typeName) =>
            (T) Enum.Parse(typeof(T), FirstCharToUpper(typeName));

        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input[0].ToString().ToUpper() + input.Substring(1)
            };
    }
}
