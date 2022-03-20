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
        public event UnityAction Event;

        public void Raise()
        {
            if (Event != null)
            {
                // TODO:    Remove the float parameter. Time is globally accesible, so we can access it
                //          from TimlineEventRecorder.
                Event.Invoke();
            }
            else
            {
                _logger.Log("A rewind event occured, but no listeners are configured.", name, LogLevel.Warning);
            }
        }

        public event UnityAction<float> RewindRequested;
        public event UnityAction RewindCancellationRequested;
        public event UnityAction RewindCompleted;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
    }
}
