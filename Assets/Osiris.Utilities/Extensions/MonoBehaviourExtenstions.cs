using UnityEngine;

namespace Osiris.Utilities.Extensions
{
    public static class MonoBehaviourExtenstions
    {
        public static void TryStopCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine)
        {
            if (coroutine == null)
            {
                return;
            }

            monoBehaviour.StopCoroutine(coroutine);
        }
    }
}
