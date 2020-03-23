using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RihaInterpreterLibrary.Processor;
using RihaInterpreterLibrary.Translator;

namespace RihaInterpreterLibrary
{
    public static class Compiler
    {
        public static int ActiveLineNumber { get; set; }

        /// <summary>
        /// Prepares code by removing all unnecessary stuff:
        /// tabs, extra whitespaces, comments, empty lines
        /// </summary>
        /// <param name="code">Text containing code</param>
        /// <returns>Compilation prepared code base lines</returns>
        private static List<string> GetClearCodeBase(string code)
        {
            TranslatorLibrary.RunCodeTroughTranslator(ref code);

            // Removes repeating whitespaces
            code = Utility.Utility.RemoveRepeatingSpace(code);
            code = Utility.Utility.RemoveColumnSpace(code);

            return code.Split('\n').ToList();
        }

        public static void Run(string[] rawLines)
        {
            LanguageLibrary.BuildActionDictionary();
            HeapMemory.Init();
            Output.Init();

            var lines = GetClearCodeBase(string.Join('\n', rawLines));

            new LabelProcessor().Process(lines);

            for(ActiveLineNumber = 0; ActiveLineNumber < lines.Count && ActiveLineNumber >= 0; ActiveLineNumber++)
            {
                var line = lines[ActiveLineNumber];

                if (ScopeController.DoScopesSkip(line))
                    continue;

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
