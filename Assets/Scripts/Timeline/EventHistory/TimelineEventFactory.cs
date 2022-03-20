using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineEventFactory : ITimelineEventFactory<TimelineEvent>
    {
        public TimelineEvent Create(IRewindableCommand rewindableCommand)
        {
            return new TimelineEvent(Time.time, rewindableCommand);
        }
    }
}
