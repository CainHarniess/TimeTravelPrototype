using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Logging;
using System;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class TimelineReplayPlayer : ITimelinePlayer
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [ReadOnly] [SerializeField] private bool _InProgress;
        [SerializeReference] private ListEventHistory _ReplayPlaylist;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] ReplayEventChannelSO _ReplayCompleted;

        private const string LogPrefix = "TimelineRewindPlayer";

        public TimelineReplayPlayer(ReplayEventChannelSO replayCompleted, UnityConsoleLogger logger)
        {
            _logger = logger;
        }

        public void Build(ListEventHistory eventHistory)
        {
            _ReplayPlaylist = eventHistory;
        }

        public bool CanPlay()
        {
            if (_InProgress)
            {
                _logger.Log("Rewind already in progress.", LogPrefix, LogLevel.Warning);
                return false;
            }

            if (_ReplayPlaylist.Count == 0)
            {
                _logger.Log("Rewind playlist is empty.", LogPrefix, LogLevel.Warning);
                return false;
            }
            return true;
        }

        public IEnumerator Play(float initialWaitTime)
        {
            _InProgress = true;
            int initialRewindCount = _ReplayPlaylist.Count;

            float waitTime = initialWaitTime;

            for (int i = 0; i < initialRewindCount; i++)
            {
                ITimelineEvent timelineEventToUndo = _ReplayPlaylist.Peek();
                yield return new WaitForSeconds(waitTime);

                timelineEventToUndo.Redo();
                _ReplayPlaylist.Pop();

                if (_ReplayPlaylist.Count == 0)
                {
                    _ReplayCompleted.Raise();
                    yield break;
                }

                waitTime = _ReplayPlaylist.Peek().Time - timelineEventToUndo.Time;
            }
        }

        public bool CanStop()
        {
            _logger.Log("Stop replay request received.", LogPrefix);
            if (!_InProgress)
            {
                _logger.Log("Replay not in progress.", LogPrefix, LogLevel.Warning);
                return false;
            }
            return true;
        }

        public void Stop()
        {
            _InProgress = false;
            _ReplayPlaylist.Clear();
        }
    }
}
