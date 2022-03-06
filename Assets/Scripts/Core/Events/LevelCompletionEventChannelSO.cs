using Osiris.TimeTravelPuzzler.EditorCustomisation;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler
{
    [CreateAssetMenu(fileName = AssetMenu.LevelCompletionEventChannelFileName,
        menuName = AssetMenu.LevelCompletionEventChannelPath)]
    public class LevelCompletionEventChannelSO : ScriptableObject
    {
        public event UnityAction LevelCompleted;

        public void RaiseLevelCompletion()
        {
            if (LevelCompleted != null)
            {
                LevelCompleted.Invoke();
            }
            else
            {
                Debug.LogWarning("Level completion event raised, but no listeners are configured.");
            }
        }
    }
}
