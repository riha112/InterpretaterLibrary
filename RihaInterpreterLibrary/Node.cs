using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public class Node
    {
        public ValueType Type { get; set; }
        public object Value { get; set; }

        public Node() { }

        public Node(object value)
        {
            Value = value;
            NodeController.SetTypeFromValue(this);
        }

        public Node(object value, ValueType type)
        {
            Value = value;
            Type = type;
            if (type == ValueType.Auto)
                NodeController.SetTypeFromValue(this);
        }
    }
}
