using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;
using Osiris.Utilities.Logging;
using System.Collections.Generic;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Timing;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        private ITimelineEventFactory<ITimelineEvent> _eventFactory;
        private IRecorder _eventRecorder;

        private IEnumerator _coroutine;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [SerializeReference] private ListEventHistory _EventHistory;
        [SerializeReference] private ITimelinePlayer _rewindPlayback;
        [SerializeReference] private IStopwatch _rewindProgressStopwatch;
        [SerializeReference] private ITimelinePlayer _replayPlayback;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;
        [SerializeField] private ReplayEventChannelSO _ReplayCompleted;

        private void Awake()
        {
            ConfigureLogger();
            ConfigureEventRecorder();
            ConfigurePlaybacks();
        }

        private void Start()
        {
            StartRecording();
        }

        private void StartRewindProcess()
        {
            _rewindPlayback.Build(_EventHistory);
            if (!_rewindPlayback.CanPlay())
            {
                _logger.Log("Rewind request rejected.", gameObject.name);
                return;
            }

            _logger.Log("Rewind request approved.", gameObject.name);

            StopRecording();
            _RewindCompleted.Event += StopRewindStartReplay;
            _coroutine = _rewindPlayback.Play(Time.time);
            StartCoroutine(_coroutine);
        }

        private void StopRewindProcess()
        {
            if (!_rewindPlayback.CanStop())
            {
                _logger.Log("Rewind cancellation request rejected.", gameObject.name);
                return;
            }
            
            _rewindPlayback.Stop();
            if (_coroutine == null)
            {
                _logger.Log("No coroutine to stop.", name, LogLevel.Error);
                return;
            }
            StopCoroutine(_coroutine);
            _RewindCompleted.Event -= StopRewindStartReplay;
        }

        private void StopRewindStartReplay()
        {
            StopRewindProcess();
            StartReplayProcess(_rewindProgressStopwatch.DeltaTime);
        }

        private void StartReplayProcess(float initialWaitTime)
        {
            if (!_replayPlayback.CanPlay())
            {
                return;
            }
            _ReplayCompleted.Event += StopReplayProcess;
            _coroutine = _replayPlayback.Play(initialWaitTime);
            StartCoroutine(_coroutine);
        }

        private void StopReplayProcess()
        {
            _logger.Log("Stop replay request received.", gameObject.name);
            if (!_replayPlayback.CanStop())
            {
                _logger.Log("Replay cancellation request rejected.", gameObject.name);
                return;
            }

            _replayPlayback.Stop();
            _ReplayCompleted.Event += StopReplayProcess;
            StartRecording();

            if (_coroutine == null)
            {
                _logger.Log("No coroutine to stop.", name, LogLevel.Error);
                return;
            }
            StopCoroutine(_coroutine);
        }

        #region Recording
        private void StartRecording()
        {
            _RecordableActionOccurred.Event += Record;
            _eventRecorder.StartRecording();
        }

        private void Record(IRewindableCommand command)
        {
            _eventRecorder.Record(command);
        }

        private void StopRecording()
        {
            _RecordableActionOccurred.Event -= Record;
            _eventRecorder.StopRecording();
        } 
        #endregion

        #region Initialisation
        private void ConfigurePlaybacks()
        {
            _replayPlayback = new TimelineReplayPlayer(_ReplayCompleted, _logger);
            _rewindProgressStopwatch = new Stopwatch();
            _rewindPlayback = new TimelineRewindPlayer(_replayPlayback,
                                                     _rewindProgressStopwatch,
                                                     _RewindCompleted,
                                                     _logger);
        }

        private void ConfigureEventRecorder()
        {
            _EventHistory = new ListEventHistory(new List<ITimelineEvent>(50));
            _eventFactory = new TimelineEventFactory();
            _eventRecorder = new TimelineEventRecorder(_EventHistory, _eventFactory);
        }

        private void ConfigureLogger()
        {
            if (_logger == null)
            {
                _logger = ScriptableObject.CreateInstance<NullConsoleLogger>();
            }
        } 
        #endregion

        private void OnEnable()
        {
            _PlayerRewindRequested.Event += StartRewindProcess;
            _PlayerRewindCancelled.Event += StopRewindStartReplay;
        }

        private void OnDisable()
        {
            _PlayerRewindRequested.Event -= StartRewindProcess;
            _PlayerRewindCancelled.Event -= StopRewindStartReplay;
        }
    }
}
