using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RihaInterpreterLibrary.Translator;

namespace RihaInterpreterLibrary
{
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

            _translatorList = _translatorList.OrderBy(x => x.PriorityId).ToList();
        }

        public static void RunCodeTroughTranslator(ref string code)
        {
            if(_translatorList == null || _translatorList.Count == 0)
                BuildTranslatorDictionary();
            code = _translatorList.Aggregate(code, (current, translator) => translator.Translate(current));
        }
    }
}
