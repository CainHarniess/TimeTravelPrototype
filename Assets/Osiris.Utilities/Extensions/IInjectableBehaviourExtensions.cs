using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.Utilities.Extensions
{

    public static class IInjectableBehaviourExtensions
    {
        public static bool IsInjectionPresent<T>(this IInjectableBehaviour behaviour, T field, string fieldName,
                                                 LogLevel logLevel = LogLevel.Warning)
            where T : Object
        {
            string editorFieldName = fieldName.ToEditorName();
            if (field != null)
            {
                return true;
            }

            string message = string.Format(GenericMessages.MissingInjection, editorFieldName);
            UnityConsoleLogger.LogAtLevel(message, logLevel, behaviour.GameObjectName);
            return false;
        }

        public static void AddComponentInjectionIfNotPresent<T>(this IInjectableBehaviour behaviour, ref T field,
                                                                     string fieldName,
                                                                     LogLevel logLevel = LogLevel.Warning)
            where T : Component
        {
            if (behaviour.IsInjectionPresent(field, fieldName, logLevel))
            {
                return;
            }

            field = behaviour.GameObject.GetComponent<T>();
            UnityConsoleLogger.LogAtLevel("Component injection added at run time.",
                                          logLevel,
                                          behaviour.GameObjectName);
        }

        public static void AddComponentInjectionByTagIfNotPresent<T>(this IInjectableBehaviour behaviour, ref T field,
                                                                     string fieldName, string tag,
                                                                     LogLevel logLevel = LogLevel.Warning)
            where T : Component
        {
            if (behaviour.IsInjectionPresent(field, fieldName, logLevel))
            {
                return;
            }

            field = GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
            UnityConsoleLogger.LogAtLevel("Component injection added at run time.",
                                          logLevel,
                                          behaviour.GameObjectName);
        }
    }
}
