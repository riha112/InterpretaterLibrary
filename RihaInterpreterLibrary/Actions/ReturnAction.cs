using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class ReturnAction : IAction
    {
        public int ArgumentCount { get; } = 0;
        public string ActionName { get; } = "return";
        private const string ValidationPattern = @"return";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            if (GotoAction.LastGotoLine != -1)
            {
                Compiler.ActiveLineNumber = GotoAction.LastGotoLine;
                GotoAction.GoToMemory.RemoveAt(GotoAction.GoToMemory.Count - 1);
            }
            else
            {
                Compiler.ActiveLineNumber = int.MaxValue;
            }
            return null;
        }
    }
}
