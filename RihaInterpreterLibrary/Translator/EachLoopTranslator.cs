using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    public class EachLoopTranslator : ITranslator
    {
        private const string CapturePattern = @"\s*\@each\([\@\:\<\>\+\<\-\,\.\=\w\d\s]*\|[\:\<\>\+\<\-\,\.\=\w\d\s]*\)\s*";

        public string Translate(string code)
        {
            return Regex.Replace(code, CapturePattern, m =>
            {
                // Gets value without each loop stuff
                var value = m.ToString().Replace("\n", "");
                value = value.Substring(6, value.Length - 7);

                var parts = value.Split('|');

                var tmpVariableName = HeapMemory.GetHashedVariableName();

                var output = $"\nset {tmpVariableName} as number: -1";
                output += $"\nopen while: {tmpVariableName} < (array.length:{parts[1]}) + -1";
                output += $"\n{tmpVariableName} += 1";
                output += $"\nset {parts[0]} as reference: array.get: {parts[1]}: {tmpVariableName}\n";
                return output;
            });
        }
    }
}
