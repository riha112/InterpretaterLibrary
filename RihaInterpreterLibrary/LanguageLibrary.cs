using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RihaInterpreterLibrary.Actions;

namespace RihaInterpreterLibrary
{
    public static class LanguageLibrary
    {
        private static Dictionary<string, IAction> _actionDictionary;

        public static IAction GetActionByName(string name) => _actionDictionary.ContainsKey(name) ? _actionDictionary[name] : null;

        public static void BuildActionDictionary()
        {
            _actionDictionary = new Dictionary<string, IAction>();

            // Loads all action types from directory Actions
            var actionsTypes = new List<Type>();

            var namespacePath = $"{nameof(RihaInterpreterLibrary)}.{nameof(RihaInterpreterLibrary.Actions)}";

            var libraries = new List<string>
            {
                namespacePath,
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Compare)}",     // Compare library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Arithmetic)}",  // Arithmetic library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Array)}",       // Array library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Text)}"         // String library
            };
            foreach (var library in libraries)
            {
                actionsTypes.AddRange(
                    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == library)
                );
            }

            // Initializes all actions into dictionary
            foreach (var actionType in actionsTypes)
            {
                if(!typeof(IAction).IsAssignableFrom(actionType))
                    continue;

                var action = (IAction) Activator.CreateInstance(actionType);
                _actionDictionary.Add(action.ActionName, action);
            }
        }
    }
}
