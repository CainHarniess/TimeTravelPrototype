using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Extensions;
using System;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class TimelineEventRecorder : IRecorder
    {
        private readonly ITimelineEventFactory<ITimelineEvent> _eventFactory;
        private readonly ILogger _logger;
        private IStackable<ITimelineEvent> _eventHistory;

        public TimelineEventRecorder(IStackable<ITimelineEvent> eventHistory,
                                     ITimelineEventFactory<ITimelineEvent> eventFactory)
        {
            _eventHistory = eventHistory;
            _eventFactory = eventFactory;
        }

        public TimelineEventRecorder(IStackable<ITimelineEvent> eventHistory,
                                     ITimelineEventFactory<ITimelineEvent> eventFactory,
                                     ILogger logger)
            : this(eventHistory, eventFactory)
        {
            _logger = logger;
        }

        [SerializeField] private bool _IsRecording = false;

        public void Record(IRewindableCommand rewindableCommand)
        {
            if (!_IsRecording)
            {
                return;
            }
            ITimelineEvent eventToRecord = _eventFactory.Create(rewindableCommand);
            _eventHistory.Push(eventToRecord);
        }

        public void StartRecording()
        {
            _IsRecording.ChangeStatusWithException(true);
        }

        public void StopRecording()
        {
            _IsRecording.ChangeStatusWithException(false);
        }
    }
}
