using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.RewindEventChannelFileName, menuName = AssetMenu.RewindEventChannelPath)]
    public class RewindEventChannelSO : DescriptionSO
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        public event UnityAction Event;

        public void Raise()
        {
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                _Logger.Log("A rewind event occured, but no listeners are configured.", name, LogLevel.Warning);
            }
        }
    }
}
