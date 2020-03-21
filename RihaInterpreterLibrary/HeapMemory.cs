using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class HeapMemory
    {
        public static Dictionary<string, Node> Heap { get; set; }
        public static List<Scope> Scopes { get; set; }

        public static void Init()
        {
            Scopes = new List<Scope>();
            Heap = new Dictionary<string, Node>();
        }

        public static void UpdateAdd(string name, Node value)
        {
            if (Heap.ContainsKey(name))
                Heap[name] = value;
            else
                Heap.Add(name, value);
        }
    }
}
