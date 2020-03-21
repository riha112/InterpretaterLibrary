using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class Output
    {
        public static List<string> OutputLines { get; set; }

        public static void Init() => OutputLines = new List<string>();
    }
}
