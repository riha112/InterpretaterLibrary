using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Compare
{
    public class EqualValueAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "compare.equal_value";
        private const string ValidationPattern = @"compare.equal_value";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;

        public Node Execute(string[] actionInParts, List<Node> variables) =>
            new Node(variables[^1].Value.ToString().Equals(variables[^2].Value.ToString()), ValueType.Boolean);
    }
}
