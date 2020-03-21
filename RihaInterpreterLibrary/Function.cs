using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public class Function
    {
        public string Name { get; set; }
        public List<Node> Parameters { get; set; }
        public string[] Lines { get; set; }
    }
}
