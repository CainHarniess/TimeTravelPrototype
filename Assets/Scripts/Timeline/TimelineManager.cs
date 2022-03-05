using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private Timeline _timeline = new Timeline();

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _rewindInProgress;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _rewindCompleteEventChannel;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;
        [SerializeField] private RewindEventChannelSO _startRewindEventChannel;

        private void Record(IRewindableCommand command)
        {
            if (_rewindInProgress)
            {
                Debug.Log("Rewind in progress. Action not recorded");
                return;
            }
            TimelineEvent timelineEvent = new TimelineEvent(Time.time, command);
            _timeline.Push(timelineEvent);
        }

        private void QueuePreviousActionForRewind(float rewindStartTime)
        {
            if (_timeline.Count == 0)
            {
                Debug.Log("No actions to undo. Rewind not initiated.");
                _rewindCompleteEventChannel.NotifyRewindCompletion();
                _rewindInProgress = false;
                return;
            }

            _rewindInProgress = true;
            StartCoroutine(UndoAction(rewindStartTime, _timeline.Peek(), QueuePreviousActionForRewind));
        }

        private IEnumerator UndoAction(float rewindStartTime,
                                       TimelineEvent timelineEventToUndo,
                                       Action<float> onUndone)
        {
            float rewindWaitTime = rewindStartTime - timelineEventToUndo.EventTime;
            yield return new WaitForSeconds(rewindWaitTime);
            timelineEventToUndo.Undo();
            _timeline.Pop();
            onUndone(timelineEventToUndo.EventTime);
        }

        private void OnEnable()
        {
            _timelineEventChannel.Event += Record;
            _startRewindEventChannel.RewindRequested += QueuePreviousActionForRewind;
        }

        private void OnDisable()
        {
            _timelineEventChannel.Event -= Record;
            _startRewindEventChannel.RewindRequested -= QueuePreviousActionForRewind;
        }
    }
}
