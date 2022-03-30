using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.PlayTestSupport;
using OUL = Osiris.Utilities.Logging;
using Osiris.Utilities.Timing;
using UnityEngine;
using UnityEngine.TestTools;

namespace Osiris.TimeTravelPuzzler.Timeline.PlayTests
{
    public class TimelineRewindPlayerPlayTests
    {
        private ITimelinePlayer _replayPlayerSub;
        private IStopwatch _stopwatchSub;
        private IEventChannelSO _rewindCompletedChannelSub;
        private OUL.ILogger _loggerSub;
        private ListEventHistory _listEventHistorySub;
        private GameObject _gameObject;
        private TestRoutineRunner _coroutineRunner;

        private ITimelineEvent _event1;
        private ITimelineEvent _event2;

        private TimelineRewindPlayer _rewindPlayer;

        [SetUp]
        public void Initialise()
        {
            _replayPlayerSub = Substitute.For<ITimelinePlayer>();
            _stopwatchSub = Substitute.For<IStopwatch>();
            _rewindCompletedChannelSub = Substitute.For<IEventChannelSO>();
            _loggerSub = Substitute.For<OUL.ILogger>();

            _event1 = Substitute.For<ITimelineEvent>();
            _event2 = Substitute.For<ITimelineEvent>();
            var eventList = new List<ITimelineEvent>()
            {
                _event1,
                _event2,
            };
            _listEventHistorySub = new ListEventHistory(eventList);

            _rewindPlayer = new TimelineRewindPlayer(_replayPlayerSub,
                                                     _stopwatchSub,
                                                     _rewindCompletedChannelSub,
                                                     _loggerSub);
            _rewindPlayer.Build(_listEventHistorySub);

            _gameObject = new GameObject("Dummy GameObject");
            _coroutineRunner = _gameObject.AddComponent<TestRoutineRunner>();
        }

        [UnityTest]
        public IEnumerator CanPlay_ShouldReturnFalseIfRewindInProgress()
        {
            _coroutineRunner.StartCoroutine(_rewindPlayer.Play(Time.time));
            yield return null;
            Assert.False(_rewindPlayer.CanPlay());
            yield return null;
        }

        [UnityTest]
        public IEnumerator CanStop_ShouldReturnTrueIfPlaying()
        {
            _coroutineRunner.StartCoroutine(_rewindPlayer.Play(Time.time));
            yield return null;
            Assert.True(_rewindPlayer.CanStop());
            yield return null;
        }


        [UnityTest]
        public IEnumerator Play_ShouldUndoBothEvents()
        {
            _event1.Time.Returns(0);
            _event2.Time.Returns(0);


            _coroutineRunner.StartCoroutine(_rewindPlayer.Play(0));
            yield return new WaitForSeconds(0.25f);
            _event1.Received(1).Undo();
            _event2.Received(1).Undo();
        }

        [UnityTest]
        public IEnumerator Play_ShouldOnlyUndoFirstEvent()
        {
            _event1.Time.Returns(1);
            _event2.Time.Returns(2);

            IEnumerator rewindRoutine = _rewindPlayer.Play(3);
            _coroutineRunner.StartCoroutine(rewindRoutine);

            yield return new WaitForSeconds(1);

            _event2.Received(1).Undo();
            _event1.DidNotReceive().Undo();

            _coroutineRunner.StopCoroutine(rewindRoutine);
        }
    }
}
