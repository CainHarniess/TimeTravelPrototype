using NSubstitute;
using NUnit.Framework;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;
using System.Collections.Generic;

namespace Osiris.TimeTravelPuzzler.Timeline.Tests
{
    [Obsolete("ListTimeline is not longer in use. Tests are therefore unnecessary.")]
    public class ListTimelineTests
    {
        private ITimelineEvent _timelineEvent;
        private ListTimeline _eventHistory;

        [SetUp]
        public void Initialise()
        {
            _timelineEvent = Substitute.For<ITimelineEvent>();
            _eventHistory = new ListTimeline(new List<ITimelineEvent>(3));
        }

        [Test]
        public void Push_ShouldIncreaseEventHistorySize()
        {
            _eventHistory.Push(_timelineEvent);

            Assert.AreEqual(1, _eventHistory.Count);
        }

        [Test]
        public void Peek_ShouldMaintainEventHistorySize()
        {
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Peek();

            Assert.AreEqual(3, _eventHistory.Count);
        }

        [Test]
        public void Peek_ShouldReturnLatestEntry()
        {
            _timelineEvent.Time.Returns(1);

            ITimelineEvent action2 = Substitute.For<ITimelineEvent>();
            action2.Time.Returns(2);

            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(action2);
            ITimelineEvent testaction = _eventHistory.Peek();

            Assert.AreEqual(2, testaction.Time);
        }

        [Test]
        public void Peek_ShouldThrowExceptionWhenEmpty()
        {
            Assert.Throws<IndexOutOfRangeException>(() => _eventHistory.Peek());
        }

        [Test]
        public void Pop_ShouldDecreaseEventHistorySize()
        {
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Pop();

            Assert.AreEqual(2, _eventHistory.Count);
        }

        [Test]
        public void Pop_ShouldThrowExceptionWhenEmpty()
        {
            Assert.Throws<IndexOutOfRangeException>(() => _eventHistory.Pop());
        }

        [Test]
        public void Clear_ShouldSetCountToZero()
        {
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Push(_timelineEvent);
            _eventHistory.Clear();

            Assert.AreEqual(0, _eventHistory.Count);
        }
    }
}
