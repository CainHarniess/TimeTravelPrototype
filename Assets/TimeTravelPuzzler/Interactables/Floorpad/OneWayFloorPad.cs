using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPad : WeightedFloorPad
    {
        public OneWayFloorPad(IFloorPad floorPadBehaviour, ILogger logger, string gameObjectName,
                              PressSpriteEffect spriteEffect)
            : base(floorPadBehaviour, logger, gameObjectName, spriteEffect)
        {

        }
    }
}