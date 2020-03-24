using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Arithmetic
{
    public class ModAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "arithmetic.module";
        private const string ValidationPattern = @"arithmetic.module";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var output = new Node
            {
                Type = ValueType.Number,
                Value = NodeController.NodeAsNumber(variables[^1]) % NodeController.NodeAsNumber(variables[^2])
            };
            return output;
        }
    }
}
