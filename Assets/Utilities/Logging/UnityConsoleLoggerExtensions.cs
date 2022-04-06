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
    }
}
