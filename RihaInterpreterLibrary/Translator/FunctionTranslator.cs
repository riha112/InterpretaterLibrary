using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{

    public class FunctionTranslator: ITranslator
    {
        private const string CapturePattern =
            @"\s*@func\s+[\w\d]*\s*\([\w\d\|]*\)\s*[\w\d\s\~\!\@\#\$\%\^\&\*\(\)\-\+\{\}\:\""\'\<\>\?\/\|\\\[\]\=]*@endfs*";

        public int PriorityId { get; set; } = 13;
        private Dictionary<string, (string code,string[] param)> _functionInfo;
        public string Translate(string code)
        {
            _functionInfo = new Dictionary<string, (string, string[])>();
            
            // Attaches break point so that main code would never
            // reach function part of code
            code += "\nreturn";

            // Finds all function declarations - registers parameters, rewrites by
            // adding label: function_name and return, moves to end of code
            code = Regex.Replace(code, CapturePattern, m =>
            {
                var text = m.ToString();
                var lines = m.ToString().Split("\n").ToList();

                if(string.IsNullOrWhiteSpace(lines[0]))
                    lines.RemoveAt(0);

                // Gets clear first line without @func and last )
                var line = lines[0].Replace(" ", "");
                line = line.Substring(5, line.Length - 6);

                // Gets function name
                var name = line.Substring(0, line.IndexOf('('));
                // Gets content of ()
                line = line.Substring(name.Length + 1, line.Length - name.Length - 1);

                // Splits parameters by |
                var parameters = line.Split("|");

                lines[0] = $"\nlabel:{name}";
                lines[^1] = "return";

                _functionInfo.Add(name, (string.Join('\n', lines), parameters));
                
                return "";
            });

            // Replaces function calls with goto: function_name
            foreach (var info in _functionInfo)
            {
                code = Regex.Replace(code, @"\s*" + info.Key + @"\s*\([\d\w\|]*\)s*", m =>
                {
                    var text = m.ToString().Replace(" ", "").Replace("\n", "");
                    var parameters = text.
                        Substring(info.Key.Length + 1, text.Length - info.Key.Length - 2).
                        Split('|');

                    var output = "";
                    for (var i = 0; i < info.Value.param.Length; i++)
                        output += $"\nset {info.Value.param[i]} as reference: {parameters[i]}";
                    output += $"\ngoto:{info.Key}";
                    return output;
                });
                code += info.Value.code;
            }



            return code;
        }
    }
}
