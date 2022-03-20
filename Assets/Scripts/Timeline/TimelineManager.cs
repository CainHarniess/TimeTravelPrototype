using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;
using Osiris.Utilities.Logging;
using System.Collections.Generic;
using Osiris.TimeTravelPuzzler.Timeline.Core;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeReference] private ListEventHistory _EventHistory;
        private ITimelineEventFactory<ITimelineEvent> _eventFactory;
        private IRecorder _eventRecorder;

        private Stopwatch _rewindProgressStopwatch;
        [SerializeReference] private ITimelinePlayer _rewindPlayer;
        [SerializeReference] private ITimelinePlayer _replayPlayer;
        private IEnumerator _coroutine;

        IEnumerator _currentCoroutine;
        
        /*[SerializeReference] */private ListTimeline _rewindTimeline;
        /*[SerializeReference] */private ListTimeline _replayTimeline;
        
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [SerializeField] private Stopwatch _rewindStopwatch = new Stopwatch();

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;
        [SerializeField] private ReplayEventChannelSO _ReplayCompleted;

        private void Awake()
        {
            if (_logger == null)
            {
                _logger = new NullConsoleLogger();
            }

            _rewindTimeline = new ListTimeline(new List<ITimelineEvent>(50));
            _replayTimeline = new ListTimeline(new List<ITimelineEvent>(50));

            _EventHistory = new ListEventHistory(new List<ITimelineEvent>(50));
            _eventFactory = new TimelineEventFactory();
            _eventRecorder = new TimelineEventRecorder(_EventHistory, _eventFactory);

            _replayPlayer = new TimelineReplayPlayer(_ReplayCompleted, _logger);
            _rewindProgressStopwatch = new Stopwatch();
            _rewindPlayer = new TimelineRewindPlayer(_replayPlayer,
                                                     _rewindProgressStopwatch,
                                                     _RewindCompleted,
                                                     _logger);
        }

        private void Start()
        {
            _eventRecorder.StartRecording();
        }

        private void StartRewindProcess()
        {
            _rewindPlayer.Build(_EventHistory);
            if (!_rewindPlayer.CanPlay())
            {
                _logger.Log("Rewind request rejected.", gameObject.name);
                return;
            }

            _logger.Log("Rewind request approved.", gameObject.name);

            // TODO:    Subscribe to rewind completed event here instead of in OnEnable.

            _eventRecorder.StopRecording();
            _coroutine = _rewindPlayer.Play(Time.time);
            StartCoroutine(_coroutine);
        }

        private void StopRewindProcess()
        {
            if (!_rewindPlayer.CanStop())
            {
                _logger.Log("Rewind cancellation request rejected.", gameObject.name);
                return;
            }
            
            _rewindPlayer.Stop();
            if (_coroutine == null)
            {
                _logger.Log("No coroutine to stop.", name, LogLevel.Error);
                return;
            }
            StopCoroutine(_coroutine);
            // TODO:    Unsubscribe from rewind completed event here instead of in OnDisable.
        }

        private void StopRewindStartReplay()
        {
            StopRewindProcess();
            StartReplayProcess(_rewindProgressStopwatch.DeltaTime);
        }

        private void StartReplayProcess(float initialWaitTime)
        {
            if (!_replayPlayer.CanPlay())
            {
                return;
            }
            // TODO:    Subscribe to replay completed event here instead of in OnEnable.
            _coroutine = _replayPlayer.Play(initialWaitTime);
            StartCoroutine(_coroutine);
        }

        private void StopReplayProcess()
        {
            if (!_replayPlayer.CanStop())
            {
                _logger.Log("Replay cancellation request rejected.", gameObject.name);
                return;
            }

            _replayPlayer.Stop();
            _eventRecorder.StartRecording();

            if (_coroutine == null)
            {
                _logger.Log("No coroutine to stop.", name, LogLevel.Error);
                return;
            }
            StopCoroutine(_coroutine);
            // TODO:    Unsubscribe from rewind completed event here instead of in OnDisable.
        }

        private void Record(IRewindableCommand command)
        {
            _eventRecorder.Record(command);
        }

        private void OnEnable()
        {
            _RecordableActionOccurred.Event += Record;
            _PlayerRewindRequested.Event += StartRewindProcess;
            _PlayerRewindCancelled.Event += StopRewindStartReplay;
            _RewindCompleted.Event += StopRewindStartReplay;
            _ReplayCompleted.Event += StopReplayProcess;
        }

        private void OnDisable()
        {
            _RecordableActionOccurred.Event -= Record;
            _PlayerRewindRequested.Event -= StartRewindProcess;
            _PlayerRewindCancelled.Event -= StopRewindStartReplay;
            _RewindCompleted.Event -= StopRewindStartReplay;
            _ReplayCompleted.Event -= StopReplayProcess;
        }
    }
}
