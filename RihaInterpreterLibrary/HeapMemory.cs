using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    /// <summary>
    /// Stores all runtime variables (nodes), that are officially initialized.
    /// </summary>
    public static class HeapMemory
    {
        // Extra security to ensure that all randomly generated variables
        // are truly unique 
        private static uint _hashCountId = 0;
        public static Dictionary<string, Node> Heap { get; set; }
        
        public static void Init()
        {
            Heap = new Dictionary<string, Node>();
        }

        /// <summary>
        /// Registers new node if doesn't exist,
        /// Updates value if exists.
        /// </summary>
        public static void UpdateAdd(string name, Node value)
        {
            if (Heap.ContainsKey(name))
                Heap[name] = value;
            else
                Heap.Add(name, value);
        }

        /// <summary>
        /// Stores value inside memory, by creating
        /// randomly generated name
        /// </summary>
        /// <returns>Generated values name</returns>
        public static string StoreHashed(Node value)
        {
            var name = GetHashedVariableName();
            UpdateAdd(name, value);
            return name;
        }

        /// <returns>Randomly generated word with "___var__" ar start</returns>
        public static string GetHashedVariableName() => 
            "___var___" + Utility.Utility.RandomString(12) + _hashCountId++;
    }
}
