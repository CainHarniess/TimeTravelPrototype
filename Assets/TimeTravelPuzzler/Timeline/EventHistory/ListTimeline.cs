using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    [Obsolete("No longer in use following on from timeline refactor. Use ListEventHistory class instead.", true)]
    public class ListTimeline : IStackable<ITimelineEvent>
    {
        [SerializeReference] private List<ITimelineEvent> _eventHistory;

        public ListTimeline(List<ITimelineEvent> timeline)
        {
            _eventHistory = timeline;
        }

        public int Count => _eventHistory.Count;

        public void Push(ITimelineEvent item)
        {
            _eventHistory.Add(item);
        }

        public ITimelineEvent Peek()
        {
            if (_eventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }
            return _eventHistory[_eventHistory.Count - 1];
        }

        public ITimelineEvent Pop()
        {
            if (_eventHistory.Count == 0)
            {
                throw new IndexOutOfRangeException("There are no actions in the event history.");
            }

            int index = _eventHistory.Count - 1;
            ITimelineEvent output = _eventHistory[_eventHistory.Count - 1];
            _eventHistory.RemoveAt(index);
            return output;
        }

        public void Clear()
        {
            _eventHistory.Clear();
        }
    }
}
