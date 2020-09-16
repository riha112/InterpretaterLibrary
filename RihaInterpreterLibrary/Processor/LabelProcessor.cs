using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Processor
{
    /// <summary>
    /// Creates register with all label locations in code
    /// </summary>
    public class LabelProcessor : IProcessor
    {
        public static Dictionary<string, int> Labels;
        private const string Id = "label:";

        public LabelProcessor()
        {
            Labels = new Dictionary<string, int>();
        }

        public void Process(List<string> lines)
        {
            if(lines == null)
                return;

            var labelSize = Id.Length;
            var spanOfId = Id.AsSpan();

            // Goes through out each line checking if stars with "label:" if so registers it
            for (var i = 0; i < lines.Count; i++)
            {
                // Works faster than indexof 
                var line = lines[i];
                if (line.Length < labelSize)
                    continue;
                // We create a span of character (better performance then substring, as it doesn't store anything in heap)
                var firstPart = line.AsSpan(0, labelSize);
                if (firstPart.Equals(spanOfId, StringComparison.Ordinal))
                {
                    var key = line.Substring(labelSize, line.Length - labelSize);
                    if(Labels.ContainsKey(key))
                        throw new Exception($"Label with name: {key}, already exists.");
                    Labels.Add(key, i);
                }
            }

        }
    }
}
