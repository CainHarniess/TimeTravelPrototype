using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        private Stack<TimelineEvent> _eventHistory = new Stack<TimelineEvent>(50);

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;

        private event Action<float> TimelineEventUndone;

        private void Record(IRewindableCommand command)
        {
            TimelineEvent timelineEvent = new TimelineEvent(Time.time, command);
            _eventHistory.Push(timelineEvent);
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
            TimelineEvent previousEvent = _eventHistory.Pop();

            StartCoroutine(UndoEventAfterSeconds(rewindStartTime, previousEvent));
        }

        private IEnumerator UndoEventAfterSeconds(float rewindStartTime, TimelineEvent timelineEventToUndo)
        {
            TimelineEventUndone += WaitThenUndoEvent;
            
            float rewindWaitTime = rewindStartTime - timelineEventToUndo.EventTime;

            yield return new WaitForSeconds(rewindWaitTime);
            
            timelineEventToUndo.Undo();

            if (_eventHistory.Count == 0)
            {
                Debug.Log("Event stack is empty. No more events to undo.");
                yield break;
            }

            TimelineEventUndone.Invoke(timelineEventToUndo.EventTime);
            TimelineEventUndone -= WaitThenUndoEvent;
        }

        private void OnEnable()
        {
            _timelineEventChannel.Event += Record;
            _rewindEventChannel.Event += StartRewind;
        }

        private void OnDisable()
        {
            _timelineEventChannel.Event -= Record;
            _rewindEventChannel.Event -= StartRewind;
        }
    }
}
