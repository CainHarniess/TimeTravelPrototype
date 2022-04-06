using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class OneWayWeightedFloorPadBehaviour : WeightedFloorPadBehaviour
    {
        protected override IFactory<IWeightedFloorPad, IFloorPad> GetFactory()
        {
            return new OneWayFloorPadFactory(Logger, GameObjectName, SpriteEffect, Pressed, Released);
        }
    }
}