using Osiris.Utilities.Logging;
using System;

namespace Osiris.Utilities.Extensions
{
    public static class BoolExtensions
    {
        public static void ChangeStatus(this ref bool status, bool value)
        {
            if (status == value)
            {
                return;
            }
            status = value;
        }

        private const string StatusChangeException = "Current value is equal to the new value."
                                                     + " Check the use of ChangeStatusWithException.";

        public static void ChangeStatusWithException(this ref bool status, bool value)
        {
            if (status == value)
            {
                UnityConsoleLogger.LogAtLevel(StatusChangeException, LogLevel.Error);
            }
            status = value;
        }
    }
}
