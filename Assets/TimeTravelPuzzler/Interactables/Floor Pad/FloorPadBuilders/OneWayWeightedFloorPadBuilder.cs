using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.OneWayFloorPadBuilderFileName, menuName = AssetMenu.OneWayFloorPadBuilderPath)]
    public class OneWayWeightedFloorPadBuilder : FloorPadBuilderSO
    {
        public override IWeightedFloorPad Build()
        {
            return new OneWayWeightedFloorPad(Behaviour, Logger, GameObjectName, PressValidator,
                                              ReleaseValidator, PressedChannel, ReleasedChannel);
        }
    }
}