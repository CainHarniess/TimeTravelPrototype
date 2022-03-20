using NSubstitute;
using NUnit.Framework;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using System;

namespace Osiris.TimeTravelPuzzler.Timeline.Tests
{
    public class TimelineEventRecorderTests
    {
        private IStackable<ITimelineEvent> _eventHistorySub;
        private ITimelineEventFactory<ITimelineEvent> _eventFactorySub;
        private IRewindableCommand _rewindableCommand;

        private TimelineEventRecorder _timelineRecorder;

        [SetUp]
        public void Initialise()
        {
            _eventHistorySub = Substitute.For<IStackable<ITimelineEvent>>();
            _eventFactorySub = Substitute.For<ITimelineEventFactory<ITimelineEvent>>();
            _rewindableCommand = Substitute.For<IRewindableCommand>();
            _timelineRecorder = new TimelineEventRecorder(_eventHistorySub, _eventFactorySub);
        }

        [Test]
        public void Record_ShouldCallEventHistoryPushWhenRecording()
        {
            _timelineRecorder.StartRecording();
            _timelineRecorder.Record(_rewindableCommand);
            _eventHistorySub.Received().Push(Arg.Any<ITimelineEvent>());
        }

        [Test]
        public void StartRecording_ShouldThrowExceptionIfAlreadyRecording()
        {
            Assert.Throws<ArgumentException>(() => _timelineRecorder.StopRecording());
        }

        [Test]
        public void Record_ShouldNotCallEventHistoryPushWhenNotRecording()
        {
            _timelineRecorder.StartRecording();
            _timelineRecorder.StopRecording();
            _timelineRecorder.Record(_rewindableCommand);

            _eventHistorySub.DidNotReceiveWithAnyArgs().Push(Arg.Any<ITimelineEvent>());
        }

        [Test]
        public void StopRecording_ShouldThrowExceptionIfNotAlreadyRecording()
        {
            _timelineRecorder.StartRecording();
            Assert.Throws<ArgumentException>(() => _timelineRecorder.StartRecording());
        }

        [Test]
        public void Record_ShouldNotRecordByDefault()
        {
            _timelineRecorder.Record(_rewindableCommand);
            _eventHistorySub.DidNotReceive().Push(Arg.Any<ITimelineEvent>());
        }
    }
}
