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

        [SerializeField] IEnumerator _currentCoroutine;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _rewindInProgress;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;

        [Header(InspectorHeaders.BroadcastsOnListensTo)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;

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

        private void IterateRewind(float rewindStartTime)
        {
            if (_timeline.Count == 0)
            {
                if (!_rewindInProgress)
                {
                    Debug.Log("No actions to undo. Rewind not initiated.");
                    return;
                }

                Debug.Log("No more actions to undo. Rewind completed.");
                _rewindEventChannel.NotifyRewindCompletion();
                _rewindInProgress = false;
                return;
            }

            _rewindInProgress = true;
            _currentCoroutine = QueueUndo(rewindStartTime, _timeline.Peek(), IterateRewind);
            StartCoroutine(_currentCoroutine);
        }

        private IEnumerator QueueUndo(float rewindStartTime,
                                      TimelineEvent timelineEventToUndo,
                                      Action<float> onUndone)
        {
            float rewindWaitTime = rewindStartTime - timelineEventToUndo.EventTime;
            yield return new WaitForSeconds(rewindWaitTime);
            
            timelineEventToUndo.Undo();
            _timeline.Pop();
            
            StopCoroutine(_currentCoroutine);
            onUndone(timelineEventToUndo.EventTime);
        }

        private void OnRewindCancelled()
        {
            Debug.Log("Rewind cancellation request received.");

            if (!_rewindInProgress)
            {
                Debug.Log("No rewind to cancel.");
                return;
            }

            _rewindInProgress = false;
            StopCoroutine(_currentCoroutine);
            _timeline.Clear();
        }

        private void OnEnable()
        {
            _timelineEventChannel.Event += Record;
            _rewindEventChannel.RewindRequested += IterateRewind;
            _rewindEventChannel.RewindCancellationRequested += OnRewindCancelled;
        }

        private void OnDisable()
        {
            _timelineEventChannel.Event -= Record;
            _rewindEventChannel.RewindRequested -= IterateRewind;
            _rewindEventChannel.RewindCancellationRequested += OnRewindCancelled;
        }
    }
}
