using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class WeightedFloorPadCommand : IRewindableCommand
    {
        private IFloorPad _floorPad;
        private int _weightChange;
        private readonly IEventChannelSO _releasedChannel;
        private readonly IEventChannelSO _pressedChannel;
        private readonly PressSpriteEffect _spriteEffect;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurredChannel;
        private readonly ILogger _logger;
        private readonly string _gameObjectName;
        private bool _shouldRecordExecution;

        public WeightedFloorPadCommand(IFloorPad floorPad, int additionalWeight, IEventChannelSO pressedChannel,
                                       IEventChannelSO releasedChannel, PressSpriteEffect spriteEffect,
                                       IEventChannelSO<IRewindableCommand> recordableActionOccurredChannel,
                                       ILogger logger, string gameObjectName)
        {
            _floorPad = floorPad;
            _weightChange = additionalWeight;
            _pressedChannel = pressedChannel;
            _releasedChannel = releasedChannel;
            _spriteEffect = spriteEffect;
            _recordableActionOccurredChannel = recordableActionOccurredChannel;
            _logger = logger;
            _gameObjectName = gameObjectName;
            _shouldRecordExecution = true;
        }

        protected IFloorPad FloorPad => _floorPad;
        protected int WeightChange => _weightChange;
        public abstract ICommand Inverse { get; }
        public abstract string Description { get; }
        protected IEventChannelSO ReleasedChannel => _releasedChannel;
        protected IEventChannelSO PressedChannel => _pressedChannel;
        protected PressSpriteEffect SpriteEffect => _spriteEffect;
        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurredChannel => _recordableActionOccurredChannel;
        protected ILogger Logger => _logger;
        public bool ShouldRecordExecution { get => _shouldRecordExecution; set => _shouldRecordExecution = value; }
        protected string GameObjectName => _gameObjectName;


        public abstract bool CanExecute(object parameter = null);

        public abstract void Execute(object parameter = null);
    }
}