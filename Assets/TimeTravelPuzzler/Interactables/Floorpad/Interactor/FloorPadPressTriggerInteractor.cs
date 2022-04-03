using Osiris.TimeTravelPuzzler.Interactables.Core;
using O = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadPressTriggerInteractor : IFloorPadInteractor
    {
        private readonly O.ILogger _logger;
        private readonly string _gameObjectName;

        public FloorPadPressTriggerInteractor(O.ILogger logger, string gameObjectName)
        {
            _logger = logger;
            _gameObjectName = gameObjectName;
        }

        public void Interact(IFloorPad floorPad, int candidateWeight)
        {
            if (!floorPad.CanPress(candidateWeight))
            {
                _logger.Log("Press request rejected.", _gameObjectName);
                return;
            }

            _logger.Log("Press approval received.", _gameObjectName);
            floorPad.Press();
        }
    }
}