using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    /// <summary>
    /// Implements command that can be executed inside interpreter
    /// </summary>
    public interface IAction
    {
        // Minimum count of arguments to be passed
        public int ArgumentCount { get; }
        // Calling name of action in code
        string ActionName { get; }
        // Validator, checks if action calling is defined correctly
        bool IsValid(string action);
        // Runs action
        Node Execute(string[] actionInParts, List<Node> variables);
    }
}
