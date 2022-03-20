using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.ReplayEventChannelFileName, menuName = AssetMenu.ReplayEventChannelPath)]
    public class ReplayEventChannelSO : DescriptionSO
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;

        public event UnityAction Event;

        private const string LogPrefix = "ReplayEventChannelSO";

        public void Raise()
        {
            _logger.Log("Replay event received on channel.", name);
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                _logger.Log("A replay event was raised, but no listeners are configured.", name);
            }
        }
    }
}
