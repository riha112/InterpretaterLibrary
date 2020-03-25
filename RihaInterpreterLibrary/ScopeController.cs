using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class ScopeController
    {
        public static List<Scope> Scopes { get; set; } = new List<Scope>();

        public static bool DoScopesSkip(string line)
        {
            if (Scopes.Count > 0)
            {
                var scope = Scopes[^1];
                // If condition is false 
                if ((bool)scope.Parameter.Value == false)
                {
                    var words = line.Split(' ');
                    if (words[0] == "open")
                        scope.InsideScopesOpen++;

                    if (words[0] == "close")
                    {
                        if (scope.InsideScopesOpen == 0)
                            Scopes.Remove(scope);
                        scope.InsideScopesOpen--;
                    }
                    return true;
                }

                // If reached end of scope
                if (line == "close")
                {
                    Scopes.Remove(scope);

                    // If scope with type check (if) then scope is closed
                    // as it has no repeatability.
                    if (scope.Type == ScopeType.Check)
                        return true;

                    // If "while" then go back to start until
                    // parameter changes to false
                    Compiler.ActiveLineNumber = scope.StartLine - 1;
                    return true;
                }
            }

            return false;
        }

    }
}
