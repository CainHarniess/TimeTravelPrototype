using UnityEngine;

namespace Osiris.Utilities.Extensions
{
    public static class MonoBehaviourExtenstions
    {
        public static void TryStopCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine)
        {
            if (coroutine == null)
            {
                Debug.Log("coroutine is null");
                return;
            }
            Debug.Log("coroutine is not null");

            monoBehaviour.StopCoroutine(coroutine);
        }
    }
}
