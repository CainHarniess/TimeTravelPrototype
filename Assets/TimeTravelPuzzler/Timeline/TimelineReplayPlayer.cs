using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Logging;
using System;
using System.Collections;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class TimelineReplayPlayer : ITimelinePlayer
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private ILogger _Logger;
        [ReadOnly] [SerializeField] private bool _InProgress;
        [SerializeReference] private ListEventHistory _ReplayPlaylist;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] ReplayEventChannelSO _ReplayCompleted;

        private const string LogPrefix = "TimelineRewindPlayer";

        public TimelineReplayPlayer(ReplayEventChannelSO replayCompleted, ILogger logger)
        {
            _ReplayCompleted = replayCompleted;
            _Logger = logger;
        }

        public void Build(ListEventHistory eventHistory)
        {
            _ReplayPlaylist = eventHistory;
        }

        public bool CanPlay()
        {
            if (_InProgress)
            {
                _Logger.Log("Rewind already in progress.", LogPrefix, LogLevel.Warning);
                return false;
            }

            if (_ReplayPlaylist.Count == 0)
            {
                _Logger.Log("Rewind playlist is empty.", LogPrefix, LogLevel.Warning);
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
                yield return new WaitForSeconds(waitTime);

                ITimelineEvent timelineEventToUndo = _ReplayPlaylist.Pop();
                timelineEventToUndo.Redo();

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
            _Logger.Log("Stop replay request received.", LogPrefix);
            if (!_InProgress)
            {
                _Logger.Log("Replay not in progress.", LogPrefix, LogLevel.Warning);
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
