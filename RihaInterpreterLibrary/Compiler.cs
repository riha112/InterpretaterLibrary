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
        /// <summary>
        /// Stores id of currently running line
        /// </summary>
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
            // Loads all actions from directory "action" and creates registry
            LanguageLibrary.BuildActionDictionary();

            // Initializes memory where all variables are stored 
            HeapMemory.Init();

            // Resets output list (contains all print and print_line outputs)
            Output.Init();

            // Gets translated code
            var lines = GetClearCodeBase(string.Join('\n', rawLines));
            
            // Runs code through processor - to register all labels in code
            new LabelProcessor().Process(lines);

            for(ActiveLineNumber = 0; ActiveLineNumber < lines.Count && ActiveLineNumber >= 0; ActiveLineNumber++)
            {
                var line = lines[ActiveLineNumber];

                // Checks if currently in scope if in then checks weather or not to execute current line
                // or close the scope
                if (ScopeController.DoScopesSkip(line))
                    continue;

                // Stores previous actions result (in same line), as all actions are executed from right to left (seperated by ":"),
                var lineActionHistory = new List<Node>();
                // Seperets line into actions as all actions are divided with ":" symbol
                var actions = line.Split(":").Reverse().ToList();
                // Runs action and stores result in "lineActionHistory", so that next action can aces its value
                foreach (var action in actions)
                    lineActionHistory.Add(RunAction(action, lineActionHistory));
            }
        }

        /// <summary>
        /// Transforms passed action into node. By using one of three methods:
        /// * If action is registered in actionLibrary -> executes action from library
        /// * Checks whether action is stored variable
        /// * Transforms action into new node
        /// </summary>
        private static Node RunAction(string actionText, List<Node> history)
        {
            var words = actionText.Split();
            
            // If action is built-in action
            if (LanguageLibrary.GetActionByName(words[0]) is {} action)
            {
                // If action is correctly formatted and has correct amount parameters 
                if (action.IsValid(actionText) && history.Count >= action.ArgumentCount)
                    return action.Execute(words, history);
                return null;    // Incorrect action
            }

            // If action is stored variable
            if (HeapMemory.Heap.ContainsKey(words[0]))
                return HeapMemory.Heap[words[0]];

            // Else its a variable => creates new variable
            return new Node(actionText);
        }
    }
}
