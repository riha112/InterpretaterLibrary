using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RihaInterpreterLibrary
{
    public static class ArrayController
    {
        private const string ArrayPattern = @"\[.*\]";

        public static bool StringIsArray(string value) => Utility.Utility.MatchesRegex(ArrayPattern, value);

        public static List<Node> StringToArray(string value)
        {
            var nonBracketString = value.Substring(1, value.Length - 2);
            var variables = nonBracketString.Split(',');

            var openCount = 0;
            var currentVariable = new List<string>();
            var realVariableList = new List<string>();
            foreach (var variable in variables)
            {
                currentVariable.Add( variable );

                if (variable[0] == '[')
                    openCount++;
                else if (variable.Last() == ']' && openCount > 0)
                    openCount--;
                
                if (openCount > 0)
                    continue;

                realVariableList.Add(string.Join(',',currentVariable));
                currentVariable = new List<string>();
            }

            return realVariableList.Select(variable => new Node(variable)).ToList();
        }
    }
}
