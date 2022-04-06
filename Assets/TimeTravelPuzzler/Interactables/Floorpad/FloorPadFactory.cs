using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation;
using Osiris.Utilities;
using Osiris.Utilities.Events;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadFactory : IFactory<IWeightedFloorPad, IFloorPad>
    {
        private readonly ILogger _logger;
        private readonly string _gameObjectName;
        private readonly IFloorPadSpriteHandler _spriteHandler;
        private readonly IEventChannelSO _Pressed;
        private readonly IEventChannelSO _Released;

        public FloorPadFactory(ILogger logger, string gameObjectName, IFloorPadSpriteHandler spriteHandler,
                               IEventChannelSO pressed, IEventChannelSO released)
        {
            _logger = logger;
            _gameObjectName = gameObjectName;
            _spriteHandler = spriteHandler;
            _Pressed = pressed;
            _Released = released;
        }

        public ILogger Logger => _logger;

        public string GameObjectName => _gameObjectName;

        public IFloorPadSpriteHandler SpriteHandler => _spriteHandler;

        public IEventChannelSO Pressed => _Pressed;

        public IEventChannelSO Released => _Released;

        public virtual IWeightedFloorPad Create(IFloorPad floorPadBehaviour)
        {
            var pressValidator = new FloorPadPressValidator(floorPadBehaviour, _logger, _gameObjectName);
            var releaseValidator = new FloorPadReleaseValidator(floorPadBehaviour, _logger, _gameObjectName);
            return new WeightedFloorPad(floorPadBehaviour, _logger, _gameObjectName, _spriteHandler, pressValidator,
                                        releaseValidator, _Pressed, _Released);
        }
    }
}