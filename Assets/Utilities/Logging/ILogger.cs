using UnityEngine;

namespace Osiris.Utilities.Logging
{
    public interface ILogger
    {
        bool DisplayLogging { get; }
        void Log(string message, GameObject sender = null, LogLevel logLevel = LogLevel.Info);
        void Log(string message, string senderName = null, LogLevel logLevel = LogLevel.Info);
    }

    public struct ILoggerToolTips
    {
        public const string ToolTip = "The logging instance to be used for this instance of this class.";
    }
}