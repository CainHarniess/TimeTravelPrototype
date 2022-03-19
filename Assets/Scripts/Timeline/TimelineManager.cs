using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Core.Logging;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private UnityConsoleLogger _logger;
        IEnumerator _currentCoroutine;

        [SerializeField] private Timeline _timeline = new Timeline();

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private bool _displayLogging = false;
        [ReadOnly] [SerializeField] private bool _rewindInProgress;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;

        [Header(InspectorHeaders.BroadcastsOnListensTo)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;

        private void Awake()
        {
            if (_logger == null)
            {
                _logger = new NullConsoleLogger();
            }
        }

        private void Record(IRewindableCommand command)
        {
            if (_rewindInProgress)
            {
                _logger.Log("Rewind in progress. Action not recorded", gameObject);
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
                    _logger.Log("No actions to undo. Rewind not initiated.", gameObject);
                    return;
                }

                _logger.Log("No more actions to undo. Rewind completed.", gameObject);
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
            _logger.Log("Rewind cancellation request received.", gameObject);

            if (!_rewindInProgress)
            {
                _logger.Log("No rewind to cancel.", gameObject);
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
