using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Array
{
    public class LengthAction : IAction
    {
        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "array.length";
        private const string ValidationPattern = @"array.length";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var output = new Node
            {
                Type = ValueType.Number,
                Value = ((List<Node>)variables[^1].Value).Count
            };
            return output;
        }
    }
}
