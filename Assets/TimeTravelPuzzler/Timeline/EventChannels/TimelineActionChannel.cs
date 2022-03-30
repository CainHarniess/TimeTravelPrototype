using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.TimelineEventChannelFileName, menuName = AssetMenu.TimelineEventChannelPath)]
    public class TimelineActionChannel : DescriptionSO, IEventChannelSO<IRewindableCommand>
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        public event UnityAction<IRewindableCommand> Event;

        public void Raise(IRewindableCommand command)
        {
            if (Event != null)
            {
                Event.Invoke(command);
            }
            else
            {
                _Logger.Log("A timeline event occured, but no listeners are configured.", name);
            }
        }
    }
}
