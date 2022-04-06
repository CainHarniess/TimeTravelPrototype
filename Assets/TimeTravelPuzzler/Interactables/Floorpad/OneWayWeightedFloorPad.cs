using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Validation;
using System;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [Serializable]
    public class OneWayWeightedFloorPad : WeightedFloorPad
    {
        public OneWayWeightedFloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                                      IFloorPadSpriteHandler spriteEffect, IValidator<int> pressValidator,
                                      IValidator<int> releaseValidator, IEventChannelSO pressed,
                                      IEventChannelSO released)
            : base(floorPadBehaviour, logger, gameObjectName, spriteEffect, pressValidator, releaseValidator, pressed,
                   released)
        {
            
        }

        public override void Release()
        {
            IsPressed = false;
            SpriteEffect.OnRelease();
        }
    }
}