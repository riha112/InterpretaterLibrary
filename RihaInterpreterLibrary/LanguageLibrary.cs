using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RihaInterpreterLibrary.Actions;

namespace RihaInterpreterLibrary
{
    /// <summary>
    /// Contains list of all actions in project.
    /// </summary>
    public static class LanguageLibrary
    {
        private static Dictionary<string, IAction> _actionDictionary;

        public static IAction GetActionByName(string name) => _actionDictionary.ContainsKey(name) ? _actionDictionary[name] : null;

        /// <summary>
        /// Loads all action types and then initializes them into _actionDictionary variable, for latter access
        /// </summary>
        public static void BuildActionDictionary()
        {
            _actionDictionary = new Dictionary<string, IAction>();

            // Loads all action types from directory Actions
            var actionsTypes = new List<Type>();

            // Action folder
            var namespacePath = $"{nameof(RihaInterpreterLibrary)}.{nameof(RihaInterpreterLibrary.Actions)}";
            
            // Sub-folders and main-folder
            var libraries = new List<string>
            {
                namespacePath,
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Compare)}",     // Compare library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Arithmetic)}",  // Arithmetic library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Array)}",       // Array library
                $"{namespacePath}.{nameof(RihaInterpreterLibrary.Actions.Text)}"         // String library
            };

            // Loads all actions from previously defined directories
            foreach (var library in libraries)
            {
                actionsTypes.AddRange(
                    Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == library)
                );
            }

            // Initializes all actions into dictionary
            foreach (var actionType in actionsTypes)
            {
                // If action contains IAction interface
                if(!typeof(IAction).IsAssignableFrom(actionType))
                    continue;

                var action = (IAction) Activator.CreateInstance(actionType);
                _actionDictionary.Add(action.ActionName, action);
            }
        }
    }
}
