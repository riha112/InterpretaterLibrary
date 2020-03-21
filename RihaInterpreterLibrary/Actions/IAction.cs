using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public interface IAction
    {
        public int ArgumentCount { get; }
        string ActionName { get; }
        bool IsValid(string action);
        Node Execute(string[] actionInParts, List<Node> variables);
    }
}
