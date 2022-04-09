using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation
{
    public abstract class FloorPadValidator
    {
        private readonly IWeightedFloorPad _floorPad;
        private readonly ILogger _logger;
        private readonly string _gameObjectName;

        protected FloorPadValidator(IWeightedFloorPad floorPad, ILogger logger, string gameObjectName)
        {
            _floorPad = floorPad;
            _logger = logger;
            _gameObjectName = gameObjectName;
        }

        protected IWeightedFloorPad FloorPad => _floorPad;
        protected ILogger Logger => _logger;
        protected string GameObjectName => _gameObjectName;

        public abstract bool IsValid(int weightChange);
    }
}