using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.ScriptableObjects;
using Osiris.Utilities.Validation;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadBuilderSO : DescriptionSO
    {
        protected FloorPadBuilderSO()
        {

        }

        public IWeightedFloorPad Behaviour { get; private set; }
        public FloorPadBuilderSO WithBehaviour(IWeightedFloorPad behaviour)
        {
            Behaviour = behaviour;
            return this;
        }

        public ILogger Logger { get; private set; }
        public FloorPadBuilderSO WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }

        public string GameObjectName { get; private set; }
        public FloorPadBuilderSO WithGameObjectName(string gameObjectName)
        {
            GameObjectName = gameObjectName;
            return this;
        }

        public IValidator<int> PressValidator { get; private set; }
        public FloorPadBuilderSO WithPressValidator(IValidator<int> pressValidator)
        {
            PressValidator = pressValidator;
            return this;
        }

        public IValidator<int> ReleaseValidator { get; private set; }
        public FloorPadBuilderSO WithReleaseValidator(IValidator<int> releaseValidator)
        {
            ReleaseValidator = releaseValidator;
            return this;
        }

        public IEventChannelSO PressedChannel { get; private set; }
        public FloorPadBuilderSO WithPressedChannel(IEventChannelSO pressedChannel)
        {
            PressedChannel = pressedChannel;
            return this;
        }

        public IEventChannelSO ReleasedChannel { get; private set; }
        public FloorPadBuilderSO WithReleasedChannel(IEventChannelSO releasedChannel)
        {
            ReleasedChannel = releasedChannel;
            return this;
        }

        public abstract IWeightedFloorPad Build();
    }
}