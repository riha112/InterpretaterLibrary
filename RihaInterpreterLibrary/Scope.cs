using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public class Scope
    {
        public ScopeType Type { get; set; }
        public Node Parameter { get; set; }
        public int StartLine { get; set; }
        public int InsideScopesOpen { get; set; } = 0;
    }
}
