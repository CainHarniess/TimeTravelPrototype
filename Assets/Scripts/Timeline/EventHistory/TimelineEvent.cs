using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [DebuggerDisplay("Event Time - {EventTime}")]
    [Serializable]
    public class TimelineEvent : ITimelineEvent
    {
        [SerializeField] private string _description = "HAHAHAHAW";
        public TimelineEvent(float eventTime, IRewindableCommand eventAction)
        {
            Time = eventTime;
            EventAction = eventAction;
            _description = $"{eventAction.Description} + {eventTime}";
        }

        public IRewindableCommand EventAction { get; }
        public float Time { get; }

        public void Undo()
        {
            EventAction.Inverse.Execute();
        }

        public void Redo()
        {
            EventAction.Execute();
        }
    }
}
