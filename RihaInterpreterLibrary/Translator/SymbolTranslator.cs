using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    public class SymbolTranslator: ITranslator
    {
        private static readonly Dictionary<string, (string splitter, string replacer)> Library = new Dictionary<string, (string, string)>()
        {
            { @"\+\=", ("+=","add") },
            { @"\-\=", ("-=","remove") },
            { @"\=",   ("=","update") },
            { @"\*",   ("*","arithmetic.multiply") },
            { @"\+",   ("+","arithmetic.sum") },

            { @"\=\=\=",   ("===","compare.equal") },
            { @"\=\=",   ("==","compare.equal_value") },
            { @"\>",   (">","compare.larger") },
            { @"\<",   ("<","compare.smaller") }
        };

        private const string CapturePattern = @"\s*\w+\s*\+\=\s*\w+\s*";

        public string Translate(string code)
        {
            foreach (var entire in Library)
            {
                code = Regex.Replace(code, @"\s*\w+\s*" +entire.Key+ @"\s*\w+\s*", m =>
                {
                    // Gets value without quotes
                    var value = m.ToString().Replace(" ", "");

                    var prefix = value[0] == '\n' ? "\n" : "";
                    var suffix = value[^1] == '\n' ? "\n" : "";

                    var parts = value.Replace("\n", "").Split(entire.Value.splitter);
                    return $"{prefix}{entire.Value.replacer}:{parts[0]}:{parts[1]}{suffix}";
                });
            }
            return code;
        }
    }
}
