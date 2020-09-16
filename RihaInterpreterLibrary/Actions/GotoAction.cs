using System;
using System.Collections.Generic;
using System.Text;
using RihaInterpreterLibrary.Processor;

namespace RihaInterpreterLibrary.Actions
{
    public class GotoAction : IAction
    {
        public static List<int> GoToMemory { get; set; } = new List<int>() { -1 };

        public static int LastGotoLine
        {
            get => GoToMemory[^1]; 
            set => GoToMemory.Add(value);
        }

        public int ArgumentCount { get; } = 1;
        public string ActionName { get; } = "goto";
        private const string ValidationPattern = @"goto";
        public bool IsValid(string action) => action.ToLower() == ValidationPattern;
        public Node Execute(string[] actionInParts, List<Node> variables)
        {
            var key = NodeController.NodeAsText(variables[^1]);
            if (!LabelProcessor.Labels.ContainsKey(key))
                return variables[^1];

            LastGotoLine = Compiler.ActiveLineNumber;
            Compiler.ActiveLineNumber = LabelProcessor.Labels[key];
            return variables[^1];
        }
    }
}
