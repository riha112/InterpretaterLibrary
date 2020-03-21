using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class SetAction : IAction
    {
        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "set";

        private const string ValidationPattern = @"\s*set\s+\w+\s+as\s+\w+\s*";

        public bool IsValid(string action) => Utility.Utility.MatchesRegex(ValidationPattern, action);

        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var type = Utility.Utility.GetValueType<RihaInterpreterLibrary.ValueType>(actionInParts[3]);

            // Tries to capture value
            object value = "";
            if (variables.Count > 0)
            {
                value = variables[^1].Value;
                if (type == ValueType.Reference)
                {
                    HeapMemory.UpdateAdd(actionInParts[1], variables[^1]);
                    return variables[^1];
                }
            }

            if (type == ValueType.Reference) value = null;

            var node = new Node(value, type);
            HeapMemory.UpdateAdd(actionInParts[1], node);
            return node;
        }
    }
}
