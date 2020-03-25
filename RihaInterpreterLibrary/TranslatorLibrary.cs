using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RihaInterpreterLibrary.Translator;

namespace RihaInterpreterLibrary
{
    /// <summary>
    /// Contains list of all translators
    /// </summary>
    public static class TranslatorLibrary
    {
        private static List<ITranslator> _translatorList;

        public static void BuildTranslatorDictionary()
        {
            _translatorList = new List<ITranslator>();

            // Loads all translators types from directory translator
            var translatorTypes = new List<Type>();
            var namespacePath = $"{nameof(RihaInterpreterLibrary)}.{nameof(RihaInterpreterLibrary.Translator)}";
            translatorTypes.AddRange(
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == namespacePath)
            );

            // Initializes all actions into dictionary
            foreach (var translatorType in translatorTypes)
            {
                if (!typeof(ITranslator).IsAssignableFrom(translatorType))
                    continue;

                var action = (ITranslator)Activator.CreateInstance(translatorType);
                _translatorList.Add(action);
            }

            // Orders by priority
            _translatorList = _translatorList.OrderBy(x => x.PriorityId).ToList();
        }

        /// <summary>
        /// Runs code through-out all registered translators
        /// </summary>
        /// <param name="code">Code to be translated</param>
        public static void RunCodeTroughTranslator(ref string code)
        {
            // Rebuilds library on fail
            if(_translatorList == null || _translatorList.Count == 0)
                BuildTranslatorDictionary();

            // Runs through out all translators
            code = _translatorList.Aggregate(code, (current, translator) => translator.Translate(current));
        }
    }
}
