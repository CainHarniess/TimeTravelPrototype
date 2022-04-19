using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.Utilities.Extensions
{
    public static class MonoBehaviourExtenstions
    {
        public static void TryStopCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
            else
            {
                string message = "No rewind playback coroutine to stop.";
                UnityConsoleLogger.LogAtLevel(message, LogLevel.Warning, monoBehaviour.gameObject.name);
            }
        }
    }
}
