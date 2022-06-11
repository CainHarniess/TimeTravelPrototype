using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.FloorPadBuilderFileName, menuName = AssetMenu.FloorPadBuilderPath)]
    public class WeightedFloorPadBuilder : FloorPadBuilderSO
    {
        public override IWeightedFloorPad Build()
        {
            return new WeightedFloorPad(Behaviour, Logger, GameObjectName, PressValidator, ReleaseValidator,
                                        PressedChannel, ReleasedChannel);
        }
    }
}