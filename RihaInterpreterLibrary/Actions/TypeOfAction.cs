using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class TypeOfAction : IAction
    {
        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "type_of";

        private const string ValidationPattern = @"type_of";

        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            return new Node(variables[^1].Type.ToString(), ValueType.Text);
        }
    }
}
