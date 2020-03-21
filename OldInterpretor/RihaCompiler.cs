using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Diagnostics;
using System.Reflection;

namespace OldInterpreter
{

    public class RihaCompiler
    {
        private static Dictionary<string, RihaNode> _variableMemory;
        private static List<RihaScope> _scopes;
        public string CodeOutput;

        public int LineNumber;

        /// <summary>
        /// Splits code into lines and passes it to execution
        /// </summary>
        /// <param name="text">Raw string with code</param>
        public void Execute(string text)
        {
            text = text.Replace("<br>", "\n");
            var commands = text.Split('\r', '\n');
            Execute(commands);
        }


        public void Execute(string[] commands)
        {
            _variableMemory = new Dictionary<string, RihaNode>();
            _scopes = new List<RihaScope>();
            var commandLine = "";
            CodeOutput = "";

            try
            {
                for (LineNumber = 0; LineNumber < commands.Length; LineNumber++)
                {
                    var line = commands[LineNumber];

                    // If scope is open then tries to close it
                    if (_scopes.Count > 0)
                    {
                        var activeScope = _scopes.Last();
                        if (!MatchesRegex(line, @"\s*end\s+scope\s*"))
                        {
                            switch (activeScope.type)
                            {
                                case ScopeType.check when !(bool) activeScope.parameter.GetValue():
                                    // If scope is loop it can only be closed when passed parameter is less than 1
                                case ScopeType.loop when activeScope.parameter.GetSize() < 1:
                                    continue;
                            }
                        }
                    }

                    // Ignores comments
                    if (MatchesRegex(line, @"\s*\/\/.*"))
                        continue;

                    commandLine += line;
                    if (!IsEol(line)) 
                        continue;

                    var actions = SplitActions(commandLine);
                    ExecuteAction(actions);
                    commandLine = "";
                }
            }
            catch (Exception e)
            {
                CodeOutput += "\n<color=red><b>ERROR OCCURRED:</b></color>" + e.Message;
            }
        }

        private static List<string> SplitWords(string text)
        {
            var arrayWords = text.Split(' ');
            var words = arrayWords.ToList();
            return words.Where(word => word != "" && word != " " && word != null).ToList();
        }

        public static void AddToMemory(string key, RihaNode value) => _variableMemory.Add(key, value);

        private static List<string> SplitActions(string text) => text.Split(':').ToList();

        //END OF LINE
        public bool IsEol(string line)
        {
            var words = SplitWords(line);
            if (string.IsNullOrEmpty(line) || !words.Any()) return false;
            line = words.Last();
            return line[^1] != ':';

        }

        public void set(string action, string[] words, RihaNode[] prevActionResult)
        {
            //Pattern set [variable_key] as [variable_type]
            const string setPattern = @"\s*set\s+\w+\s+as\s+\w+\s*";
            if (!MatchesRegex(action, setPattern)) return;
            var variable = prevActionResult[^1];
            var type = GetValueType(words[3]);
            variable.SetType(type);
            _variableMemory.Add(words[1], variable);
        }

        //public void import(string action, string[] words, RihaNode[] previuseActionResult)
        //{
        //    const string setPattern = @"\s*import\s*";
        //    if (MatchesRegex(action, setPattern))
        //    {
        //        var variable = previuseActionResult[^1];
        //    }
        //}

        public void scope(string action, string[] words, RihaNode[] prevActionResult)
        {
            const string setPattern = @"\s*scope\s+\w+\s*";
            if (!MatchesRegex(action, setPattern)) return;
            var value = prevActionResult[^1];
            var type = GetScopeType(words[1]);
            if (type == ScopeType.check && value.GetNodeType() == ValueType.boolean ||
                type == ScopeType.loop && value.GetNodeType() == ValueType.number)
                _scopes.Add(new RihaScope
                {
                    type = type,
                    parameter = value,
                    startLine = LineNumber
                });
        }

        public void end(string action, string[] words, RihaNode[] prevActionResult)
        {
            const string setPattern = @"\s*end\s+scope\s*";
            if (!MatchesRegex(action, setPattern)) return;
            if (_scopes.Count <= 0) return;

            var activeScope = _scopes.Last();

            if (activeScope.type == ScopeType.loop &&
                activeScope.iteration < activeScope.parameter.GetSize() - 1)
            {
                activeScope.iteration++;
                activeScope.endLine = LineNumber;
                LineNumber = activeScope.startLine;
            }
            else
            {
                _scopes.Remove(activeScope);
            }
        }

        public void print(string action, string[] words, RihaNode[] prevActionResult)
        {
            const string printPattern = @"\s*print\s*";
            if (!MatchesRegex(action, printPattern)) return;
            var node = prevActionResult[^1];
            var value = node.GetString();
            CodeOutput += value + "\n";
        }

        public void size(string action, string[] words, RihaNode[] prevActionResult)
        {
            const string printPattern = @"\s*size\s+of\s*";
            if (!MatchesRegex(action, printPattern)) return;
            var node = prevActionResult[^1];
            CodeOutput += node.GetSize() + "\n";
        }

        public void add(string action, string[] words, RihaNode[] prevActionResult)
        {
            const string addPattern = @"\s*add\s+to\s+\w+\s*";
            if (!MatchesRegex(action, addPattern)) return;
            var variableKey = words[2];
            if (!_variableMemory.ContainsKey(variableKey)) return;
            var variable = _variableMemory[variableKey];
            variable.Add(prevActionResult[^1]);
        }

        private static bool MatchesRegex(string text, string pattern)
        {
            var r = new Regex(pattern, RegexOptions.IgnoreCase);
            return r.Match(text).Success;
        }

        private void ExecuteAction(List<string> actions)
        {
            actions.Reverse();
            var finishedActionsReturns = new List<RihaNode>();
            foreach (var action in actions)
            {
                var words = SplitWords(action).ToArray();
                var isAction = EvaluateAction(action, words, finishedActionsReturns);
                if (isAction == null)
                {
                    var functionNode = IsFunction(action, words);
                    if (functionNode != null)
                    {
                        var functions = words[0].Split('.');
                        var methodName = functionNode.GetNodeType() == ValueType.GLOBAL
                            ? "GlobalCall"
                            : functions[1].ToLower();
                        var functionMethod = functionNode.GetType().GetMethod(methodName);
                        if (functionMethod == null) continue;
                        object[] parameters;
                        if (functionNode.GetNodeType() == ValueType.GLOBAL)
                            parameters = new object[]
                            {
                                functions[1].ToLower(),
                                finishedActionsReturns.ToArray()
                            };
                        else
                            parameters = new object[]
                            {
                                finishedActionsReturns.ToArray()
                            };

                        var returnValue = (RihaNode) functionMethod.Invoke(functionNode, parameters);
                        finishedActionsReturns.Add(returnValue);
                    }
                    else
                    {
                        var actionMethod = GetType().GetMethod(words[0].ToLower());
                        if (actionMethod == null) continue;
                        object[] parameters =
                        {
                            action,
                            words,
                            finishedActionsReturns.ToArray()
                        };
                        actionMethod.Invoke(this, parameters);
                    }
                }
                else
                {
                    finishedActionsReturns.Add((RihaNode) isAction);
                }
            }
        }
        private static RihaNode IsFunction(string action, IReadOnlyList<string> words)
        {
            var functions = words[0].Split('.');
            if (functions.Length <= 0) return null;
            return _variableMemory.ContainsKey(functions[0]) ? _variableMemory[functions[0]] : null;
        }

        private object EvaluateAction(string action, IReadOnlyList<string> words, List<RihaNode> pevActions)
        {
            // Is number:
            var isNumeric = float.TryParse(action, out var n);
            if (isNumeric) return new RihaNode(ValueType.number, n);

            if (words.Count == 1)
            {
                switch (words[0].ToLower())
                {
                    //Is boolean            
                    case "true":
                        return new RihaNode(ValueType.boolean, true);
                    case "false":
                        return new RihaNode(ValueType.boolean, false);
                    case "null":
                        return new RihaNode(ValueType.tmp);
                }

            }

            //Is variable
            if (_variableMemory.ContainsKey(words[0])) return _variableMemory[words[0]];

            //Is array
            const string arrayPattern = @"\[.*\]";
            var arrayGroups = GroupMatch(action, arrayPattern);
            if (arrayGroups != null)
            {
                var arrayless = arrayGroups[0].Substring(1, arrayGroups[0].Length - 2);
                var elements = arrayless.Split(',');
                var array = new List<RihaNode>();
                foreach (var element in elements)
                {
                    var elementWords = SplitWords(element).ToArray();
                    var variable = (RihaNode) EvaluateAction(element, elementWords, null);
                    array.Add(variable);
                }

                return new RihaNode(ValueType.array, array);
            }

            //Is text:
            const string textPattern = @"\"".*?\""";
            var textGroups = GroupMatch(action, textPattern);
            if (textGroups == null) return null;
            var qouteless = textGroups[0].Substring(1, textGroups[0].Length - 2);
            return new RihaNode(ValueType.text, qouteless);

        }

        private static List<string> GroupMatch(string text, string pattern)
        {
            var r = new Regex(pattern, RegexOptions.IgnoreCase);
            var m = r.Match(text);
            if (m.Groups.Count <= 0 || m.Groups[0].Value == "") return null;
            var groups = new List<string>();
            for (var i = 0; i < m.Groups.Count; i++) groups.Add(m.Groups[i].Value);
            return groups;
        }

        private static ValueType GetValueType(string typeName) => (ValueType)Enum.Parse(typeof(ValueType), typeName);

        private static ScopeType GetScopeType(string scopeName) => (ScopeType) Enum.Parse(typeof(ScopeType), scopeName);

    }
}