using UnityEngine;

namespace Osiris.Utilities.Logging
{
    public interface ILogger
    {
        bool DisplayLogging { get; }
        void Log(string message, GameObject sender = null, LogLevel logLevel = LogLevel.Info);
    }
}