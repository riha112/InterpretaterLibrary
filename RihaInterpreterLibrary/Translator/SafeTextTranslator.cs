using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RihaInterpreterLibrary.Translator
{
    public class SafeTextTranslator : ITranslator
    {
        private const string CapturePattern = @"""[^""]*""";

        public int PriorityId { get; set; } = 0;

        public string Translate(string code)
        {
            return Regex.Replace(code, CapturePattern, m =>
            {
                // Gets value without quotes
                var value = m.ToString();
                value = value.Substring(1, value.Length - 2);
                // Stores value in memory as variable, and replaces with variables
                // access key
                var name = HeapMemory.StoreHashed(new Node
                {
                    Type = ValueType.Text,
                    Value = value
                });
                return name;
            });
        }
    }
}
