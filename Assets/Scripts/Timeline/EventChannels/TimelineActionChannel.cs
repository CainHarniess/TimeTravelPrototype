using Osiris.TimeTravelPuzzler.Core;
using Osiris.TimeTravelPuzzler.Core.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.TimelineEventChannelFileName, menuName = AssetMenu.TimelineEventChannelPath)]
    public class TimelineActionChannel : DescriptionSO
    {
        public event UnityAction<IRewindableCommand> Event;

        public void Raise(IRewindableCommand command)
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
