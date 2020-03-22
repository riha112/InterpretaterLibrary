using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    class IfTranslator : ITranslator
    {
        private const string CapturePattern = @"\s*if\s*:\s*\w*\s*";

        public string Translate(string code)
        {
            return Regex.Replace(code, CapturePattern, m =>
            {
                var value = m.ToString().Replace(" ", "").Replace("\n", "");
                value = value.Substring(3, value.Length - 3);

                return !string.IsNullOrEmpty(value) ? $"\nopen check: {value}\n" : "";
            });
        }
    }
}
