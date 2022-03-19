using Osiris.TimeTravelPuzzler.Core.Values;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core.Logging
{
    public interface ILogger
    {
        bool DisplayLogging { get; }
        void Log(string message, GameObject sender = null, LogLevel logLevel = LogLevel.Info);
    }
}