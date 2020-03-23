using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions.Array
{
    public class GetAction : IAction
    {
        public int ArgumentCount { get; } = 2;
        public string ActionName { get; } = "array.get";
        private const string ValidationPattern = @"array.get";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var id = NodeController.NodeAsNumber(variables[^2]);
            return ((List<Node>) variables[^1].Value)[(int)id];
        }
    }
}
