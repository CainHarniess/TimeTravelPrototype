﻿using UnityEngine;

namespace Osiris.Utilities.Logging
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
