using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    /// <summary>
    /// Stores all output lines from codes execution
    /// </summary>
    public static class Output
    {
        public static List<string> OutputLines { get; set; }

        public static void Init() => OutputLines = new List<string>();

        public static event EventHandler<string> AddedLineEvent;
        public static event EventHandler<string> AddedInLineEvent;

        public static void Add(string text)
        {
            AddToLine(text);
            OutputLines.Add("");    // New line break
            AddedLineEvent?.Invoke(null, text);
        }

        public static void AddInline(string text)
        {
            AddToLine(text);
            AddedInLineEvent?.Invoke(null, text);
        }

        private static void AddToLine(string text)
        {
            if (OutputLines.Count == 0)
                OutputLines.Add("");
            OutputLines[^1] += text;
        }
    }
}
