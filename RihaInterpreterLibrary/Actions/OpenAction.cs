using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Actions
{
    public class OpenAction: IAction
    {
        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "open";

        private const string ValidationPattern = @"\s*open\s+\w+\s*";

        public bool IsValid(string action) => Utility.Utility.MatchesRegex(ValidationPattern, action);

        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var type = Utility.Utility.GetValueType<ScopeType>(actionInParts[1]);

            var scope = new Scope
            {
                Type = type, 
                Parameter = variables[^1], 
                StartLine = Compiler.CurrentExecutionLine
            };
            HeapMemory.Scopes.Add(scope);

            return variables[^1];
        }
    }
}
