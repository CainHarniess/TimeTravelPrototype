using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core.Logging
{
    public class NullConsoleLogger : UnityConsoleLogger
    {
        public override void Log(string message,
                                 GameObject sender = null,
                                 LogLevel logLevel = LogLevel.Info)
        {

        }
    }
}
