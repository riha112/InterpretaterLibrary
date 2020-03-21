using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class UpdateAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "update";
        private const string ValidationPattern = @"update";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;

        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            if (variables[^1].Type == variables[^2].Type)
                variables[^1].Value = variables[^2].Value;
            return variables[^1];
        }
    }
}
