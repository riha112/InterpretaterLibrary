using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Compare
{
    public class SmallerAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "compare.smaller";
        private const string ValidationPattern = @"compare.smaller";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;

        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var output = false;

            var a = NodeController.NodeAsNumber(variables[^1]);
            var b = NodeController.NodeAsNumber(variables[^2]);
            if (a < b)
                output = true;

            return new Node(output, ValueType.Boolean);
        }
    }
}
