using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using UnityEngine;
using UnityEngine.Events;
using Osiris.TimeTravelPuzzler.Core;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.TimelineEventChannelFileName, menuName = AssetMenu.TimelineEventChannelPath)]
    public class TimelineEventChannelSO : DescriptionSO
    {
        public event UnityAction<IRewindableCommand> Event;

        public void RecordTimelineEvent(IRewindableCommand command)
        {
            if (Event != null)
            {
                Event.Invoke(command);
            }
            else
            {
                Debug.LogWarning("A timeline event occured, but no listeners are configured.");
            }
        }
    }
}