using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
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

        public override bool CanRelease()
        {
            return false;
        }

        public override void Release()
        {
            base.Release();
            Logger.Log("One-way floor pads released.", GameObjectName);
        }
    }
}