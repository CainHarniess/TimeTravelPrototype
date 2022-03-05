using Osiris.TimeTravelPuzzler.Commands;
using System.Diagnostics;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [DebuggerDisplay("Event Time - {EventTime}")]
    public class TimelineEvent
    {
        public TimelineEvent(float eventTime, IRewindableCommand eventAction)
        {
            EventTime = eventTime;
            EventAction = eventAction;
        }

        public IRewindableCommand EventAction { get; }
        public float EventTime { get; }

        public void Undo()
        {
            EventAction.Inverse.Execute();
        }
    }
}
