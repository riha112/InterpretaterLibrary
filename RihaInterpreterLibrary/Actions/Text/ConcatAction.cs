using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Text
{
    public class ConcatAction: IAction
    {
        public int ArgumentCount { get; } = 0;
        public string ActionName { get; } = "text.concat";
        private const string ValidationPattern = @"text.concat";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;

        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            variables.Reverse();
            var value = variables.Aggregate("", (current, variable) => current + NodeController.NodeAsText(variable));
            return new Node(value, ValueType.Text);
        }
    }
}
