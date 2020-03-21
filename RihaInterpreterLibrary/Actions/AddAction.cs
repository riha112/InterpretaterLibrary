using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class AddAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "add";
        private const string ValidationPattern = @"add";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            NodeController.AddToNode(variables[^1], variables[^2]);
            return variables[^1];
        }
    }
}
