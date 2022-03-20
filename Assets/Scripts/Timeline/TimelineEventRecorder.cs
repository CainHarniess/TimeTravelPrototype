using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Extensions;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class TimelineEventRecorder : IRecorder
    {
        private IStackable<ITimelineEvent> _eventHistory;
        private ITimelineEventFactory<ITimelineEvent> _eventFactory;

        public TimelineEventRecorder(IStackable<ITimelineEvent> eventHistory,
                                     ITimelineEventFactory<ITimelineEvent> eventFactory)
        {
            _eventHistory = eventHistory;
            _eventFactory = eventFactory;
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
