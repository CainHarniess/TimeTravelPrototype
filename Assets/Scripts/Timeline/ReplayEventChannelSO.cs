using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using UnityEngine;
using UnityEngine.Events;
using Osiris.TimeTravelPuzzler.Core.Logging;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.ReplayEventChannelFileName, menuName = AssetMenu.ReplayEventChannelPath)]
    public class ReplayEventChannelSO : DescriptionSO
    {
        public event UnityAction Event;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;

        public void Raise()
        {
            _logger.Log("Replay event received on channel.");
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                _logger.Log("A replay event was raised, but no listeners are configured.");
            }
        }
    }
}
