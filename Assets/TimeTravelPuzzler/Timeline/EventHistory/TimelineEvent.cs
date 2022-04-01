using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;
using System.Diagnostics;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [DebuggerDisplay("Event Time - {EventTime}")]
    [Serializable]
    public class TimelineEvent : ITimelineEvent
    {
        private readonly OUL.ILogger _logger;
        [SerializeField] private string _Description;

        public TimelineEvent(float eventTime, IRewindableCommand eventAction)
        {
            Time = eventTime;
            EventAction = eventAction;
            _Description = $"{eventAction.Description} + {eventTime}";
        }

        public TimelineEvent(float eventTime, IRewindableCommand eventAction, OUL.ILogger logger)
            : this(eventTime, eventAction)
        {
            _logger = logger;
        }

        public IRewindableCommand EventAction { get; }
        public float Time { get; }

        public void Undo()
        {
            if (!EventAction.Inverse.CanExecute())
            {
                _logger.Log("Event action not be undone.", typeof(TimelineEvent).ToString());
                return;
            }
            EventAction.Inverse.Execute();
        }

        public void Redo()
        {
            if (!EventAction.CanExecute())
            {
                _logger.Log("Event action could not be redone.", typeof(TimelineEvent).ToString());
                return;
            }
            EventAction.Execute();
        }
    }
}
