using Osiris.TimeTravelPuzzler.Core;
using UnityEngine;
using UnityEngine.Events;
using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.RewindEventChannelFileName, menuName = AssetMenu.RewindEventChannelPath)]
    public class RewindEventChannelSO : DescriptionSO
    {
        public event UnityAction<float> RewindRequested;
        public event UnityAction RewindCancellationRequested;
        public event UnityAction RewindCompleted;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;

        public void RaiseRewindRequest()
        {
            _logger.Log("Rewind request received on channel.");
            if (RewindRequested != null)
            {
                RewindRequested.Invoke(Time.time);
            }
            else
            {
                _logger.Log("A rewind was requested, but no listeners are configured.");
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
                _logger.Log("A rewind cancellation was requested, but no listeners are configured.",
                            logLevel: LogLevel.Warning);
            }
        }
    }
}
