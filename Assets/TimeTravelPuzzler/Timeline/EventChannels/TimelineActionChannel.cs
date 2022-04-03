using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.TimelineEventChannelFileName, menuName = AssetMenu.TimelineEventChannelPath)]
    public class TimelineActionChannel : EventChannelSO<IRewindableCommand>, IEventChannelSO<IRewindableCommand>
    {
        
    }
}
