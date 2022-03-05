using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class Timeline
    {
        [SerializeField] private List<TimelineEvent> _eventHistory;

        public Timeline()
        {
            _eventHistory = new List<TimelineEvent>(50);
        }

        public int Count => _eventHistory.Count;

        public void Push(TimelineEvent item)
        {
            _eventHistory.Add(item);
        }

        public TimelineEvent Peek()
        {
            if (_eventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }
            return _eventHistory[_eventHistory.Count - 1];
        }

        public TimelineEvent Pop()
        {
            if (_eventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }

            int index = _eventHistory.Count - 1;
            TimelineEvent output = _eventHistory[_eventHistory.Count - 1];
            _eventHistory.RemoveAt(index);
            return output;
        }

        public void Clear()
        {
            _eventHistory.Clear();
        }
    }
}
