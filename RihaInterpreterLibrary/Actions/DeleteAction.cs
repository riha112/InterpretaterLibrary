using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class DeleteAction: IAction
    {
        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "delete";
        private const string ValidationPattern = @"delete";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            // TODO: Add deletion
            return null;
        }
    }
}
