using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class ScopeController
    {
        public static bool DoScopesSkip(string line)
        {
            if (HeapMemory.Scopes.Count > 0)
            {
                var scope = HeapMemory.Scopes[^1];
                // If condition is false 
                if ((bool)scope.Parameter.Value == false)
                {
                    var words = line.Split(' ');
                    if (words[0] == "open")
                        scope.InsideScopesOpen++;

                    if (words[0] == "close")
                    {
                        if (scope.InsideScopesOpen == 0)
                            HeapMemory.Scopes.Remove(scope);
                        scope.InsideScopesOpen--;
                    }
                    return true;
                }

                // If reached end of scope
                if (line == "close")
                {
                    HeapMemory.Scopes.Remove(scope);

                    // If "if" check then case closed
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
