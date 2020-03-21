using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class PrintLineAction : IAction
    {
        public int ArgumentCount { get; } = 0;
        public string ActionName { get; } = "print_line";
        private const string ValidationPattern = @"print_line";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            if (variables.Count == 0)
            {
                Output.OutputLines.Add("");
                return null;
            }
            Output.OutputLines.Add(NodeController.NodeAsText(variables[^1]));
            return variables[^1];
        }
    }
}
