using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.RewindEventChannelFileName, menuName = AssetMenu.RewindEventChannelPath)]
    public class RewindEventChannelSO : DescriptionSO
    {
        public event UnityAction<float> RewindRequested;
        public event UnityAction RewindCancellationRequested;
        public event UnityAction RewindCompleted;

        public void RaiseRewindRequest()
        {
            Debug.Log("Rewind request received on channel.");
            if (RewindRequested != null)
            {
                RewindRequested.Invoke(Time.time);
            }
            else
            {
                Debug.LogWarning("A rewind was requested, but no listeners are configured.");
            }
        }

        public void RaiseRewindCancellation()
        {
            if (RewindCancellationRequested != null)
            {
                RewindCancellationRequested.Invoke();
            }
            else
            {
                Debug.LogWarning("A rewind cancellation was requested, but no listeners are configured.");
            }
        }

        public void NotifyRewindCompletion()
        {
            if (RewindCompleted != null)
            {
                RewindCompleted.Invoke();
            }
            else
            {
                Debug.LogWarning("A rewind was completed, but no listeners are configured.");
            }
        }
    }
}
