using System.Collections.Generic;

namespace RihaInterpreterLibrary.Actions.Compare
{
    public class EqualAction: IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "compare.equal";
        private const string ValidationPattern = @"compare.equal";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;

        public Node Execute(string[] actionInParts, List<Node> variables) =>
            new Node(variables[^1].Value == variables[^2].Value, ValueType.Boolean);
    }
}
