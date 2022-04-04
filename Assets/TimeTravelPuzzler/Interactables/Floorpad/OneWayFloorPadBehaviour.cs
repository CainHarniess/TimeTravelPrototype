using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPadBehaviour : WeightedFloorPadBehaviour
    {
        protected override void Awake()
        {
            Logger.Configure();
            SpriteEffect = new PressSpriteEffect(GetComponent<SpriteRenderer>());
            FloorPad = new OneWayFloorPad(this, Logger, GameObjectName, SpriteEffect);
        }
    }
}