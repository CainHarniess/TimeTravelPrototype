using UnityEngine;

namespace Osiris.Utilities.Logging
{
    [CreateAssetMenu(fileName = AssetMenu.UnityConsoleLoggerFileName,
                     menuName = AssetMenu.UnityConsoleLoggerPath)]
    public class UnityConsoleLogger : ScriptableObject, ILogger
    {
        [SerializeField] private bool _DisplayLogging;

        public bool DisplayLogging { get => _DisplayLogging; }

        public virtual void Log(string message, GameObject sender = null, LogLevel logLevel = LogLevel.Info)
        {
            if (!DisplayLogging)
            {
                return;
            }

            switch (logLevel)
            {
                case LogLevel.Error:
                    Debug.LogError(PrefixMessage(message, sender));
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(PrefixMessage(message, sender));
                    break;
                default:
                    Debug.Log(PrefixMessage(message, sender));
                    break;
            }
        }

        protected string PrefixMessage(string message, GameObject sender = null)
        {
            if (sender != null)
            {
                return $"[{sender.name}] " + message;
            }
            return message;
        }
    }

}
