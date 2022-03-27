using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.ReplayEventChannelFileName, menuName = AssetMenu.ReplayEventChannelPath)]
    public class ReplayEventChannelSO : DescriptionSO
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        public event UnityAction Event;

        public void Raise()
        {
            _Logger.Log("Replay event received on channel.", name);
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                _Logger.Log("A replay event was raised, but no listeners are configured.", name);
            }
        }
    }
}
