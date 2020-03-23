using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Processor
{
    public class LabelProcessor : IProcessor
    {
        public static Dictionary<string, int> Labels = new Dictionary<string, int>();
        private const string Id = "label:";

        public void Process(List<string> lines)
        {
            var labelSize = Id.Length;
            var spanOfId = Id.AsSpan();

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Length < labelSize)
                    continue;

                var firstPart = line.AsSpan(0, labelSize);
                if (firstPart.Equals(spanOfId, StringComparison.Ordinal))
                    Labels.Add(line.Substring(labelSize, line.Length - labelSize), i);
            }

        }
    }
}
