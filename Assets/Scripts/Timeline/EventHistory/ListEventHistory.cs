using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class ListEventHistory : IStackable<ITimelineEvent>
    {
        [SerializeField] private IList<ITimelineEvent> _EventHistory;

        public ListEventHistory(IList<ITimelineEvent> eventHistory)
        {
            _EventHistory = eventHistory;
        }

        public int Count => _EventHistory.Count;

        public void Push(ITimelineEvent item)
        {
            _EventHistory.Add(item);
        }
        public ITimelineEvent Peek()
        {
            if (_EventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }
            return _EventHistory[_EventHistory.Count - 1];
        }

        public ITimelineEvent Pop()
        {
            if (_EventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }

            int index = _EventHistory.Count - 1;
            ITimelineEvent output = _EventHistory[_EventHistory.Count - 1];
            _EventHistory.RemoveAt(index);
            return output;
        }

        public void Clear()
        {
            _EventHistory.Clear();
        }
    }
}
