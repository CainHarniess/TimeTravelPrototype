using Osiris.TimeTravelPuzzler.Interactables.Core;
using O = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadReleaseTriggerInteractor : IFloorPadInteractor
    {
        private readonly O.ILogger _logger;
        private readonly string _gameObjectName;

        public FloorPadReleaseTriggerInteractor(O.ILogger logger, string gameObjectName)
        {
            _logger = logger;
            _gameObjectName = gameObjectName;
        }

        public void Interact(IFloorPad floorPad, int candidateWeight)
        {
            if (!floorPad.CanRelease(candidateWeight))
            {
                _logger.Log("Release request rejected.", _gameObjectName);
                return;
            }

            _logger.Log("Release approval received.", _gameObjectName);
            floorPad.Release();
        }
    }
}