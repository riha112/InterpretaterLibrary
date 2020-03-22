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
                    var parts = value.Split(entire.Value.splitter);

                    // TODO: rewrite
                    var output = "";
                    if (parts[0][0] == '\n')
                    {
                        output += "\n";
                        parts[0] = parts[0].Replace("\n", "");
                    }

                    output += $"{entire.Value.replacer}:{parts[0]}:";

                    if (parts[1][^1] == '\n')
                    {
                        parts[1] = parts[1].Replace("\n", "");
                        output += parts[1];
                        output += "\n";
                    }
                    else
                    {
                        output += parts[1];
                    }
                    // -----

                    return output;
                });
            }
            return code;
        }
    }
}
