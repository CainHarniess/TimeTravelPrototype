using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Events;
using OUL = Osiris.Utilities.Logging;
using Osiris.Utilities.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
        public class TimelineRewindPlayer : ITimelinePlayer
    {
        private readonly ITimelinePlayer _replayPlayer;
        private IStopwatch _rewindStopwatch;
        private ListEventHistory _ReplayPlaylist;
        private IEventChannelSO _RewindCompleted;
        private OUL.ILogger _logger;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private ListEventHistory _RewindPlaylist;
        [ReadOnly] [SerializeField] private bool _InProgress;

        private const string LogPrefix = "TimelineRewindPlayer";

        public TimelineRewindPlayer(ITimelinePlayer replayPlayer,
                                    IStopwatch stopwatch,
                                    IEventChannelSO rewindCompleted,
                                    OUL.ILogger logger)
        {
            _replayPlayer = replayPlayer;
            _rewindStopwatch = stopwatch;
            _logger = logger;
            _RewindCompleted = rewindCompleted;
            _ReplayPlaylist = new ListEventHistory(new List<ITimelineEvent>(50));
        }

        public void Build(ListEventHistory eventHistory)
        {
            _RewindPlaylist = eventHistory;
        }

        public bool CanPlay()
        {
            if (_InProgress)
            {
                _logger.Log("Rewind already in progress.", LogPrefix, OUL.LogLevel.Warning);
                return false;
            }

            if (_RewindPlaylist.Count == 0)
            {
                _logger.Log("Rewind playlist is empty.", LogPrefix, OUL.LogLevel.Warning);
                return false;
            }
            return true;
        }

        public IEnumerator Play(float rewindStartTime)
        {
            _InProgress = true;
            int initialRewindCount = _RewindPlaylist.Count;

            float waitTime = rewindStartTime - _RewindPlaylist.Peek().Time;

            for (int i = 0; i < initialRewindCount; i++)
            {
                _rewindStopwatch.Start();

                ITimelineEvent timelineEventToUndo = _RewindPlaylist.Peek();
                _logger.Log("Wait time = " + waitTime, LogPrefix);
                yield return new WaitForSeconds(waitTime);

                timelineEventToUndo.Undo();
                _ReplayPlaylist.Push(timelineEventToUndo);
                _RewindPlaylist.Pop();

                if (_RewindPlaylist.Count == 0)
                {
                    _RewindCompleted.Raise();
                    yield break;
                }

                waitTime = timelineEventToUndo.Time - _RewindPlaylist.Peek().Time;
                _rewindStopwatch.Stop();
            }
        }

        public bool CanStop()
        {
            _logger.Log("Stop rewind request received.", LogPrefix);
            if (!_InProgress)
            {
                _logger.Log("Rewind not in progress.", LogPrefix, OUL.LogLevel.Warning);
                return false;
            }
            return true;
        }

        public void Stop()
        {
            _InProgress = false;
            _rewindStopwatch.Stop();
            _RewindPlaylist.Clear();
            // TODO: I think this should be the responsibility of the TimelineManager
            _replayPlayer.Build(_ReplayPlaylist);
        }
    }
}
