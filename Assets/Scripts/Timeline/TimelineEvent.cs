using Osiris.TimeTravelPuzzler.Commands;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [DebuggerDisplay("Event Time - {EventTime}")]
    [Serializable]
    public class TimelineEvent
    {
        [SerializeField] private string _description = "HAHAHAHAW";
        public TimelineEvent(float eventTime, IRewindableCommand eventAction)
        {
            EventTime = eventTime;
            EventAction = eventAction;
            _description = $"{eventAction.Description} + {eventTime}";
        }

        public IRewindableCommand EventAction { get; }
        public float EventTime { get; }

        public void Undo()
        {
            EventAction.Inverse.Execute();
        }
    }
}
