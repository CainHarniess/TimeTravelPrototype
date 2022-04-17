using UnityEngine;

namespace Osiris.Utilities.Logging
{
    public static class IInjectableBehaviourExtensions
    {
        public static bool IsInjectionPresent<T>(this IInjectableBehaviour behaviour, T field, string fieldName,
                                                 LogLevel logLevel = LogLevel.Warning)
        {
            if (field != null)
            {
                return true;
            }

            string message = string.Format(GenericMessages.MissingInjection,
                                           fieldName.ToEditorName());

            string messageWithPrefix = $"[{behaviour.GameObjectName}] " + message;

            LogAtLevel(messageWithPrefix, logLevel);
            return false;
        }

        public static void AddComponentInjectionIfNotPresent<T>(this IInjectableBehaviour behaviour, ref T field,
                                                                     string fieldName, GameObject gameObject,
                                                                     LogLevel logLevel = LogLevel.Warning)
            where T : Component
        {
            if (behaviour.IsInjectionPresent(field, fieldName, logLevel))
            {
                return;
            }

            field = gameObject.GetComponent<T>();
            LogAtLevel($"[{behaviour.GameObjectName}] Component injection added at run time.", logLevel);
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
            LogAtLevel($"[{behaviour.GameObjectName}] Component injection added at run time.", logLevel);
        }

        private static void LogAtLevel(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}
