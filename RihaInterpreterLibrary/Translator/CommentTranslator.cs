using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RihaInterpreterLibrary.Translator
{
    public class CommentTranslator: ITranslator
    {
        public int PriorityId { get; set; } = 5;

        public string Translate(string code)
        {
            return string.Join("\n", code.Split('\n').Where(
                l => !(string.IsNullOrEmpty(l) || string.IsNullOrWhiteSpace(l)) // , so that it doesn't contain any empty lines
            ).Where(
                l => l[0] != '#'// and doesn't contain any comments
            )).Replace("\t", "");
        }
    }
}
