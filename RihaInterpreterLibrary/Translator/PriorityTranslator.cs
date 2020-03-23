using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    public class PriorityTranslator: ITranslator
    {
        private const string CapturePattern = @"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

        public int PriorityId { get; set; } = 25;

        public string Translate(string code)
        {
            var lines = code.Split("\n");
            var newLines = new List<string>();
            foreach (var line in lines)
                newLines.Add(BuildLine(line));
            return string.Join("\n", newLines);
        }

        private string BuildLine(string line)
        {
            var newLines = new List<string>();
            line = Regex.Replace(line, CapturePattern, m =>
            {
                // Gets value without quotes
                var value = m.ToString();
                value = value.Substring(1, value.Length - 2);

                // Generate variables name
                var name = GetTempValuesName();

                newLines.Add(BuildLine(GetSettingLine(name, value)));
                return name;
            });
            newLines.Add(line);
            return string.Join('\n', newLines);
        }

        private static string GetTempValuesName() => 
            HeapMemory.GetHashedVariableName();

        private static string GetSettingLine(string name, string value) =>
            $"set {name} as auto: {value}";

    }
}
