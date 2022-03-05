using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private List<TimelineEvent> _eventHistory = new List<TimelineEvent>(50);

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _rewindCompleteEventChannel;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;
        [SerializeField] private RewindEventChannelSO _startRewindEventChannel;

        private event Action<float> TimelineEventUndone;

        private void Record(IRewindableCommand command)
        {
            TimelineEvent timelineEvent = new TimelineEvent(Time.time, command);
            Push(timelineEvent);
        }

        private void StartRewind()
        {
            if (_eventHistory.Count == 0)
            {
                return;
            }
            WaitThenUndoEvent(Time.time);
        }

        private void WaitThenUndoEvent(float rewindStartTime)
        {
            TimelineEvent previousEvent = Pop();

            StartCoroutine(UndoEventAfterSeconds(rewindStartTime, previousEvent));
        }

        private IEnumerator UndoEventAfterSeconds(float rewindStartTime, TimelineEvent timelineEventToUndo)
        {
            float rewindWaitTime = rewindStartTime - timelineEventToUndo.EventTime;
            
            yield return new WaitForSeconds(rewindWaitTime);
            
            timelineEventToUndo.Undo();

            if (_eventHistory.Count == 0)
            {
                Debug.Log("Event stack is empty. No more events to undo.");
                _rewindCompleteEventChannel.Raise();
                yield break;
            }

            TimelineEventUndone.Invoke(timelineEventToUndo.EventTime);
        }

        private void Push(TimelineEvent item)
        {
            _eventHistory.Add(item);
        }

        private TimelineEvent Pop()
        {
            if (_eventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are now actions in the event history.");
            }

            int index = _eventHistory.Count - 1;
            TimelineEvent output = _eventHistory[_eventHistory.Count - 1];
            _eventHistory.RemoveAt(index);
            return output;
        }

        private void OnEnable()
        {
            TimelineEventUndone += WaitThenUndoEvent;
            _timelineEventChannel.Event += Record;
            _startRewindEventChannel.Event += StartRewind;
        }

        private void OnDisable()
        {
            TimelineEventUndone -= WaitThenUndoEvent;
            _timelineEventChannel.Event -= Record;
            _startRewindEventChannel.Event -= StartRewind;
        }
    }
}
