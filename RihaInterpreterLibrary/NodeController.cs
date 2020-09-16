using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class NodeController
    {
        /// <summary>
        /// Assigns nodes type by checking nodes value
        /// </summary>
        public static void SetTypeFromValue(Node node)
        {
            var valueAsText = node.Value.ToString();
            // Number type
            if (double.TryParse(valueAsText, out var n))
            {
                node.Value = n;
                node.Type = ValueType.Number;
                return;
            }

            // Boolean type
            if (bool.TryParse(valueAsText, out var b))
            {
                node.Value = b;
                node.Type = ValueType.Boolean;
                return;
            }

            // Array value
            if (node.Value is List<Node>)
            {
                node.Type = ValueType.Array;
                return;
            }
            if (ArrayController.StringIsArray((string)node.Value))
            {
                node.Value = ArrayController.StringToArray((string) node.Value);
                node.Type = ValueType.Array;
                return;
            }

            // Text value
            node.Value = node.Value.ToString();
            node.Type = ValueType.Text;
        }

        public static string NodeAsText(Node target)
        {
            if (target.Type != ValueType.Array)
                return target.Value.ToString();

            var array = (List<Node>) target.Value;
            var joinedValues = string.Join(",", array.Select(NodeAsText).ToArray());
            return "[" + joinedValues + "]";
        }

        public static double NodeAsNumber(Node target) => target.Type switch
        {
            ValueType.Number => double.Parse(target.Value.ToString()),
            ValueType.Boolean => (int) target.Value,
            ValueType.Array => ((List<Node>)target.Value).Count,
            ValueType.Text => ((string)target.Value).Length,
            _ => 0
        };

        public static void AddToNode(Node target, Node value)
        {
            switch (target.Type)
            {
                case ValueType.Number:
                    target.Value = (double)target.Value + (double)value.Value;
                    break;
                case ValueType.Text:
                    target.Value += NodeAsText(value);
                    break;
                case ValueType.Array:
                    switch (value.Type)
                    {
                        case ValueType.Array:
                            ((List<Node>)target.Value).AddRange(
                                ((List<Node>)value.Value)
                            );
                            break;
                        default:
                            ((List<Node>) target.Value).Add(value);
                            break;
                    }
                    break;
            }
        }
    }
}
