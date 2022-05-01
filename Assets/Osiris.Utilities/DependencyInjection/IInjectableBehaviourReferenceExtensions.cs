using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;

namespace Osiris.Utilities.DependencyInjection
{
    public static class IInjectableBehaviourReferenceExtensions
    {
        public static bool IsInjectionPresent(this IInjectableBehaviour behaviour, IntReference field, string fieldName,
                                              LogLevel logLevel = LogLevel.Error)
        {
            string editorFieldName = fieldName.ToEditorName();

            if (!field.UseConstant)
            {
                return true;
            }

            if (field.Constant != null)
            {
                return true;
            }

            string message = string.Format(GenericMessages.MissingInjection, editorFieldName);
            UnityConsoleLogger.LogAtLevel(message, logLevel, behaviour.GameObjectName);
            return false;
        }

        public static bool IsInjectionPresent(this IInjectableBehaviour behaviour, FloatReference field, string fieldName,
                                              LogLevel logLevel = LogLevel.Error)
        {
            string editorFieldName = fieldName.ToEditorName();

            if (!field.UseConstant)
            {
                return true;
            }

            if (field.Constant != null)
            {
                return true;
            }

            string message = string.Format(GenericMessages.MissingInjection, editorFieldName);
            UnityConsoleLogger.LogAtLevel(message, logLevel, behaviour.GameObjectName);
            return false;
        }

        public static bool IsInjectionPresent(this IInjectableBehaviour behaviour, BoolReference field, string fieldName,
                                              LogLevel logLevel = LogLevel.Error)
        {
            string editorFieldName = fieldName.ToEditorName();

            if (!field.UseConstant)
            {
                return true;
            }

            if (field.Constant != null)
            {
                return true;
            }

            string message = string.Format(GenericMessages.MissingInjection, editorFieldName);
            UnityConsoleLogger.LogAtLevel(message, logLevel, behaviour.GameObjectName);
            return false;
        }
    }
}
