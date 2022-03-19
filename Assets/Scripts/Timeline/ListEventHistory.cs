using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class ListEventHistory : IStackable<TimelineEvent>
    {
        [SerializeField] private List<TimelineEvent> _EventHistory;
        public void Push(TimelineEvent item)
        {
            _EventHistory.Add(item);
        }
        public TimelineEvent Peek()
        {
            if (_EventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }
            return _EventHistory[_EventHistory.Count - 1];
        }

        public TimelineEvent Pop()
        {
            if (_EventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }

            int index = _EventHistory.Count - 1;
            TimelineEvent output = _EventHistory[_EventHistory.Count - 1];
            _EventHistory.RemoveAt(index);
            return output;
        }

        public void Clear()
        {
            _EventHistory.Clear();
        }
    }
}
