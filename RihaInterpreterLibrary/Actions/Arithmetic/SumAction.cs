using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Arithmetic
{
    public class SumAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "arithmetic.sum";
        private const string ValidationPattern = @"arithmetic.sum";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var output = new Node
            {
                Type = ValueType.Number,
                Value = NodeController.NodeAsNumber(variables[^1]) + NodeController.NodeAsNumber(variables[^2])
            };
            return output;
        }
    }
}
