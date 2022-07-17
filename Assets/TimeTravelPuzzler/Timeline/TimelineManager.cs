using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using Osiris.Utilities.Timing;
using Osiris.Utilities.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineManager : OsirisMonoBehaviour, ILoggableBehaviour
    {
        private ITimelineEventFactory<ITimelineEvent> _eventFactory;
        private IRecorder _eventRecorder;

        private Coroutine _rewindCoroutine;
        private Coroutine _replayCoroutine;
        private Coroutine _rewindTimerCoroutine;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private FloatReference _MaximumRewindTimeRef;
        [SerializeField] private FixedCoroutineCallbackTimer _FixedTimer;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private ListEventHistory _EventHistory;
        [SerializeReference] private ITimelinePlayer _RewindPlayback;
        [SerializeReference] private IStopwatch _RewindProgressStopwatch;
        [SerializeReference] private ITimelinePlayer _ReplayPlayback;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _RewindStarted;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;
        [SerializeField] private ReplayEventChannelSO _ReplayCompleted;

        [Header(InspectorHeaders.WritesTo)]
        [SerializeField] private BoolVariableSO _IsRewinding;

        public Utilities.Logging.ILogger Logger => _Logger;
        
        protected override void Awake()
        {
            base.Awake();

            _FixedTimer = new FixedCoroutineCallbackTimer(_MaximumRewindTimeRef.Value, TryStopRewindTryStartReplay);

            this.IsInjectionPresent(_Logger, nameof(_Logger));

            ConfigureEventRecorder();
            ConfigurePlaybacks();
            _IsRewinding.Value = false;
        }

        private void Start()
        {
            StartRecording();
        }

        private void TryStartRewindProcess()
        {
            _RewindPlayback.Build(_EventHistory);
            if (!_RewindPlayback.CanPlay())
            {
                Logger.Log("Rewind request rejected.", GameObjectName);
                return;
            }
            Logger.Log("Rewind request approved.", GameObjectName);

            StopRecording();
            _RewindCompleted.Event += TryStopRewindTryStartReplay;

            _RewindStarted.Raise();
            _IsRewinding.Value = true;

            _rewindTimerCoroutine = StartCoroutine(_FixedTimer.StartTimer());
            _rewindCoroutine = StartCoroutine(_RewindPlayback.Play(Time.time));
        }

        private void TryStopRewindTryStartReplay()
        {
            Debug.Log("Truing to stop rewind.");
            TryStopRewindProcess();
            TryStartReplayProcess(_RewindProgressStopwatch.DeltaTime);
        }

        private void TryStopRewindProcess()
        {
            if (!_RewindPlayback.CanStop())
            {
                Logger.Log("Rewind cancellation request rejected.", GameObjectName);
                return;
            }
            _RewindPlayback.Stop();
            _IsRewinding.Value = false;

            this.TryStopCoroutine(_rewindCoroutine);
            this.TryStopCoroutine(_rewindTimerCoroutine);
        }

        private void TryStartReplayProcess(float initialWaitTime)
        {
            if (!_ReplayPlayback.CanPlay())
            {
                return;
            }
            _ReplayCompleted.Event += TryStopReplayProcess;
            _replayCoroutine = StartCoroutine(_ReplayPlayback.Play(initialWaitTime));
        }

        private void TryStopReplayProcess()
        {
            Logger.Log("Stop replay request received.", GameObjectName);
            if (!_ReplayPlayback.CanStop())
            {
                Logger.Log("Replay cancellation request rejected.", GameObjectName);
                return;
            }

            _ReplayPlayback.Stop();
            this.TryStopCoroutine(_replayCoroutine);
            _ReplayCompleted.Event -= TryStopReplayProcess;
            StartRecording();
        }

        #region Recording
        private void StartRecording()
        {
            _eventRecorder.StartRecording();
        }

        private void Record(IRewindableCommand command)
        {
            _eventRecorder.Record(command);
        }

        private void StopRecording()
        {
            _eventRecorder.StopRecording();
        } 
        #endregion

        #region Initialisation
        private void ConfigurePlaybacks()
        {
            _ReplayPlayback = new TimelineReplayPlayer(_ReplayCompleted, Logger);
            _RewindProgressStopwatch = new Stopwatch();
            _RewindPlayback = new TimelineRewindPlayer(_ReplayPlayback,
                                                       _RewindProgressStopwatch,
                                                       _RewindCompleted,
                                                       Logger);
        }

        private void ConfigureEventRecorder()
        {
            _EventHistory = new ListEventHistory(new List<ITimelineEvent>(50));
            _eventFactory = new TimelineEventFactory(Logger);
            _eventRecorder = new TimelineEventRecorder(_EventHistory, _eventFactory, Logger);
        }
        #endregion

        private void OnEnable()
        {
            _RecordableActionOccurred.Event += Record;
            _PlayerRewindRequested.Event += TryStartRewindProcess;
            _PlayerRewindCancelled.Event += TryStopRewindTryStartReplay;
        }

        private void OnDisable()
        {
            _RecordableActionOccurred.Event -= Record;
            _PlayerRewindRequested.Event -= TryStartRewindProcess;
            _PlayerRewindCancelled.Event -= TryStopRewindTryStartReplay;
        }
    }
}
