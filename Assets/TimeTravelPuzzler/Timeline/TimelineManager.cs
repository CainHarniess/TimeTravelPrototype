using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Logging;
using Osiris.Utilities.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : MonoBehaviour
    {
        private ITimelineEventFactory<ITimelineEvent> _eventFactory;
        private IRecorder _eventRecorder;

        private IEnumerator _coroutine;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private ListEventHistory _EventHistory;
        [SerializeReference] private ITimelinePlayer _RewindPlayback;
        [SerializeReference] private IStopwatch _RewindProgressStopwatch;
        [SerializeReference] private ITimelinePlayer _ReplayPlayback;

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
            _RewindPlayback.Build(_EventHistory);
            if (!_RewindPlayback.CanPlay())
            {
                _Logger.Log("Rewind request rejected.", gameObject.name);
                return;
            }

            _Logger.Log("Rewind request approved.", gameObject.name);

            StopRecording();
            _RewindCompleted.Event += StopRewindStartReplay;
            _coroutine = _RewindPlayback.Play(Time.time);
            StartCoroutine(_coroutine);
        }

        private void StopRewindProcess()
        {
            if (!_RewindPlayback.CanStop())
            {
                _Logger.Log("Rewind cancellation request rejected.", gameObject.name);
                return;
            }
            
            _RewindPlayback.Stop();
            if (_coroutine == null)
            {
                _Logger.Log("No coroutine to stop.", name, LogLevel.Error);
                return;
            }
            StopCoroutine(_coroutine);
            _RewindCompleted.Event -= StopRewindStartReplay;
        }

        private void StopRewindStartReplay()
        {
            StopRewindProcess();
            StartReplayProcess(_RewindProgressStopwatch.DeltaTime);
        }

        private void StartReplayProcess(float initialWaitTime)
        {
            if (!_ReplayPlayback.CanPlay())
            {
                return;
            }
            _ReplayCompleted.Event += StopReplayProcess;
            _coroutine = _ReplayPlayback.Play(initialWaitTime);
            StartCoroutine(_coroutine);
        }

        private void StopReplayProcess()
        {
            _Logger.Log("Stop replay request received.", gameObject.name);
            if (!_ReplayPlayback.CanStop())
            {
                _Logger.Log("Replay cancellation request rejected.", gameObject.name);
                return;
            }

            _ReplayPlayback.Stop();
            _ReplayCompleted.Event += StopReplayProcess;
            StartRecording();

            if (_coroutine == null)
            {
                _Logger.Log("No coroutine to stop.", name, LogLevel.Error);
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
            _ReplayPlayback = new TimelineReplayPlayer(_ReplayCompleted, _Logger);
            _RewindProgressStopwatch = new Stopwatch();
            _RewindPlayback = new TimelineRewindPlayer(_ReplayPlayback,
                                                       _RewindProgressStopwatch,
                                                       _RewindCompleted,
                                                       _Logger);
        }

        private void ConfigureEventRecorder()
        {
            _EventHistory = new ListEventHistory(new List<ITimelineEvent>(50));
            _eventFactory = new TimelineEventFactory();
            _eventRecorder = new TimelineEventRecorder(_EventHistory, _eventFactory);
        }

        private void ConfigureLogger()
        {
            if (_Logger == null)
            {
                _Logger = ScriptableObject.CreateInstance<NullConsoleLogger>();
                _Logger.Configure();
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
