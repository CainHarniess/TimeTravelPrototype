using Osiris.TimeTravelPuzzler.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler
{
    [CreateAssetMenu(fileName = AssetMenu.LevelCompletionEventChannelFileName,
                     menuName = AssetMenu.LevelCompletionEventChannelPath)]
    public class LevelCompletionEventChannelSO : ScriptableObject
    {
        public event UnityAction Event;

        public void Raise()
        {
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                Debug.LogWarning("Level completion event raised, but no listeners are configured.");
            }
        }
    }
}
