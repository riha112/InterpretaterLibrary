using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RihaInterpreterLibrary.Translator;

namespace RihaInterpreterLibrary
{
    public static class Compiler
    {
        public static int CurrentExecutionLine { get; private set; }

        /// <summary>
        /// Prepares code by removing all unnecessary stuff:
        /// tabs, extra whitespaces, comments, empty lines
        /// </summary>
        /// <param name="code">Text containing code</param>
        /// <returns>Compilation prepared code base lines</returns>
        private static List<string> GetClearCodeBase(string code)
        {
            code = new SafeTextTranslator().Translate(code);
            code = code.Replace("\t", "");
            code = new ForLoopTranslator().Translate(code);
            code = new PriorityTranslator().Translate(code);
            code = new SymbolTranslator().Translate(code);
            code = new IfTranslator().Translate(code);

            // Removes repeating whitespaces
            code = Utility.Utility.RemoveRepeatingSpace(code);
            code = Utility.Utility.RemoveColumnSpace(code);

            // Splits code into lines
            var lines = code.Split('\n').Where(
                l => !(string.IsNullOrEmpty(l) || string.IsNullOrWhiteSpace(l)) // , so that it doesn't contain any empty lines
            ).Where(
                l => l[0] != '#'// and doesn't contain any comments
            ).ToList();

            return lines;
        }

        public static void Run(string[] rawLines)
        {
            LanguageLibrary.BuildActionDictionary();
            HeapMemory.Init();
            Output.Init();

            var lines = GetClearCodeBase(string.Join('\n', rawLines));
            for(var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                CurrentExecutionLine = i;

                #region  Scope

                // If scope is open
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
                            if(scope.InsideScopesOpen == 0)
                                HeapMemory.Scopes.Remove(scope);
                            scope.InsideScopesOpen--;
                        }
                        continue;
                    }

                    // If reached end of scope
                    if (line == "close")
                    {
                        HeapMemory.Scopes.Remove(scope);

                        // If "if" check then case closed
                        if (scope.Type == ScopeType.Check)
                            continue;

                        // If "while" then go back to start until
                        // parameter changes to false
                        i = scope.StartLine - 1;
                        continue;
                    }
                }

                #endregion

                var lineActionHistory = new List<Node>();
                var actions = line.Split(":").Reverse().ToList();
                foreach (var action in actions)
                    lineActionHistory.Add(RunAction(action, lineActionHistory));
            }
        }

        private static Node RunAction(string actionText, List<Node> history)
        {
            var words = actionText.Split();
            
            // If action is built-in action
            if (LanguageLibrary.GetActionByName(words[0]) is {} action)
            {
                if (action.IsValid(actionText) && history.Count >= action.ArgumentCount)
                    return action.Execute(words, history);
                return null;    // Incorrect action
            }

            // If action is stored variable
            if (HeapMemory.Heap.ContainsKey(words[0]))
                return HeapMemory.Heap[words[0]];

            // Else its an variable
            return new Node(actionText);
        }

        //private void Remove
    }
}
