using NSubstitute;
using NUnit.Framework;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using Osiris.Utilities.Timing;
using System.Collections.Generic;

namespace Osiris.TimeTravelPuzzler.Timeline.Tests
{
    public class TimelineRewindPlayerTests
    {
        private ITimelinePlayer _replayPlayerSub;
        private IStopwatch _stopwatchSub;
        private IEventChannelSO _rewindCompletedChannelSub;
        private ILogger _loggerSub;
        private TimelineRewindPlayer _rewindPlayer;
        private ListEventHistory _listEventHistorySub;

        [SetUp]
        public void Initialise()
        {
            _replayPlayerSub = Substitute.For<ITimelinePlayer>();
            _stopwatchSub = Substitute.For<IStopwatch>();
            _rewindCompletedChannelSub = Substitute.For<IEventChannelSO>();
            _loggerSub = Substitute.For<ILogger>();

            ITimelineEvent event1 = Substitute.For<ITimelineEvent>();
            ITimelineEvent event2 = Substitute.For<ITimelineEvent>();
            var eventList = new List<ITimelineEvent>()
            {
                event1,
                event2,
            };
            _listEventHistorySub = new ListEventHistory(eventList);

            _rewindPlayer = new TimelineRewindPlayer(_replayPlayerSub,
                                                     _stopwatchSub,
                                                     _rewindCompletedChannelSub,
                                                     _loggerSub);
            _rewindPlayer.Build(_listEventHistorySub);
        }

        [Test]
        public void CanPlay_ShouldReturnFalseIfRewindPlaylistEmpty()
        {
            var eventList = new List<ITimelineEvent>()
            {
            };
            _listEventHistorySub = new ListEventHistory(eventList);
            _rewindPlayer.Build(_listEventHistorySub);
            Assert.False(_rewindPlayer.CanPlay());
        }

        [Test]
        public void CanPlay_ShouldReturnTrueWhenNoRewindInProgressAndEventsToRewind()
        {
            Assert.True(_rewindPlayer.CanPlay());
        }

        [Test]
        public void CanStop_ShouldReturnFalseIfNotPlaying()
        {
            Assert.False(_rewindPlayer.CanStop());
        }

        [Test]
        public void Stop_ShouldStopStopwatch()
        {
            _rewindPlayer.Stop();
            _stopwatchSub.Received().Stop();
        }

        [Test]
        public void Stop_ShouldBuildReplayPlayer()
        {
            _rewindPlayer.Stop();
            _replayPlayerSub.Received().Build(Arg.Any<ListEventHistory>());
        }
    }
}
