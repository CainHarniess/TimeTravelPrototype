using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        IEnumerator _currentCoroutine;

        [SerializeField] private ListTimeline _rewindTimeline = new ListTimeline();
        [SerializeField] private ListTimeline _replayTimeline = new ListTimeline();

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [ReadOnly] [SerializeField] private bool _rewindInProgress;
        [ReadOnly] [SerializeField] private bool _replayInProgress;
        [SerializeField] private Stopwatch _replayStopwatch = new Stopwatch();
        [SerializeField] private Stopwatch _rewindStopwatch = new Stopwatch();

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        [Header(InspectorHeaders.BroadcastsOnListensTo)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;
        [SerializeField] private ReplayEventChannelSO _replayCompletedChannel;

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
            _rewindTimeline.Push(timelineEvent);
        }

        private void IterateRewind(float rewindStartTime)
        {
            if (_rewindTimeline.Count == 0)
            {
                if (!_rewindInProgress)
                {
                    _logger.Log("No actions to undo. Rewind not initiated.", gameObject);
                    return;
                }

                _logger.Log("No more actions to undo. Rewind completed.", gameObject);
                _rewindInProgress = false;
                IterateReplay(_rewindStopwatch.DeltaTime);
                return;
            }

            _rewindInProgress = true;
            _currentCoroutine = QueueUndo(rewindStartTime, _rewindTimeline.Peek(), IterateRewind);
            StartCoroutine(_currentCoroutine);
        }

        private IEnumerator QueueUndo(float rewindStartTime,
                                      TimelineEvent timelineEventToUndo,
                                      Action<float> onUndone)
        {
            _rewindStopwatch.Start();
            float rewindWaitTime = rewindStartTime - timelineEventToUndo.EventTime;
            yield return new WaitForSeconds(rewindWaitTime);
            
            timelineEventToUndo.Undo();
            _replayTimeline.Push(timelineEventToUndo);
            _rewindTimeline.Pop();
            _rewindStopwatch.Stop();

            StopCoroutine(_currentCoroutine);
            onUndone(_replayTimeline.Peek().EventTime);
        }

        private void IterateReplay(float waitTime)
        {
            if (_replayTimeline.Count == 0 && !_replayInProgress)
            {
                _logger.Log("No actions to replay. Replay not initiated.", gameObject);
                return;
            }

            if (_replayTimeline.Count == 0 && _replayInProgress)
            {
                _logger.Log("No more actions to replay. Replay completed.", gameObject);
                _replayInProgress = false;
                return;
            }

            _replayInProgress = true;
            _currentCoroutine = QueueRedo(waitTime, _replayTimeline.Peek(), IterateReplay);
            StartCoroutine(_currentCoroutine);
        }

        private IEnumerator QueueRedo(float waitTime,
                                      TimelineEvent timelineEventToRedo,
                                      Action<float> onRedone)
        {
            _replayStopwatch.Start();
            yield return new WaitForSeconds(waitTime);

            timelineEventToRedo.Redo();
            _replayTimeline.Pop();
            _replayStopwatch.Stop();

            StopCoroutine(_currentCoroutine);

            if (_replayTimeline.Count == 0)
            {
                _replayCompletedChannel.Raise();
                yield break;
            }
            onRedone(_replayTimeline.Peek().EventTime - timelineEventToRedo.EventTime);
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
            _rewindTimeline.Clear();
            _rewindStopwatch.Stop();

            IterateReplay(_rewindStopwatch.DeltaTime);
        }

        private void OnEnable()
        {
            _RecordableActionOccurred.Event += Record;
            _rewindEventChannel.RewindRequested += IterateRewind;
            _rewindEventChannel.RewindCancellationRequested += OnRewindCancelled;
        }

        private void OnDisable()
        {
            _RecordableActionOccurred.Event -= Record;
            _rewindEventChannel.RewindRequested -= IterateRewind;
            _rewindEventChannel.RewindCancellationRequested += OnRewindCancelled;
        }
    }
}
