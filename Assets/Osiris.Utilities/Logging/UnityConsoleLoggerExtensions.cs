using UnityEngine;

namespace Osiris.Utilities.Logging
{
    public static class UnityConsoleLoggerExtensions
    {
        public static void Configure(this UnityConsoleLogger logger)
        {
            if (logger == null)
            {
                logger = (NullConsoleLogger)ScriptableObject.CreateInstance(typeof(NullConsoleLogger));
            }
        }
    }

    public static class ILoggerExtensions
    {
        public static void Configure(this ILogger logger)
        {
            if (logger == null)
            {
                logger = (NullConsoleLogger)ScriptableObject.CreateInstance(typeof(NullConsoleLogger));
            }
        }

        public static void MissingDependencyInjection(this ILogger logger, string fieldName, string gameObjectName)
        {
            string message = string.Format(GenericMessages.MissingInjection,
                                           fieldName.ToEditorName());

            logger.Log(message, gameObjectName,LogLevel.Warning);
        }

        public static void MissingLoggerInjection(string fieldName, string gameObjectName)
        {
            string message = string.Format(GenericMessages.MissingInjection,
                                           fieldName.ToEditorName());

            Debug.LogError($"[{gameObjectName}] " + message);
        }
    }
}
