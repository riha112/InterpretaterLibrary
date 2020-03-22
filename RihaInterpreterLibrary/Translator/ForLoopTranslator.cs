using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    public class ForLoopTranslator : ITranslator
    {
        private const string CapturePattern = @"\s*for\([\@\:\<\>\+\<\-\,\.\=\w\d\s]*\|[\:\<\>\+\<\-\,\.\=\w\d\s]*\|[\:\<\>\+\<\-\,\.\=\w\d\s]*\)\s*";

        public string Translate(string code)
        {
            return Regex.Replace(code, CapturePattern, m =>
            {
                // Gets value without for loop stuff
                var value = m.ToString().Replace("\n", "");
                value = value.Substring(4, value.Length - 5);

                var parts = value.Split('|');
                var output = "";

                // Places action before loop
                if (!string.IsNullOrEmpty(parts[0]))
                    output += $"\n{parts[0]}\n";

                // Converts for to while
                output += $"open while: {parts[1]}\n";

                // Places action after loop
                if (!string.IsNullOrEmpty(parts[2]))
                    output += parts[2] + "\n";

                return output;
            });
        }
    }
}
