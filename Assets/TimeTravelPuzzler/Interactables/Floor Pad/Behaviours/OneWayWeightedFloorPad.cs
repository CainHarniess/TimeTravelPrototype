using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Validation;
using System;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [Serializable]
    public class OneWayWeightedFloorPad : WeightedFloorPad
    {
        public OneWayWeightedFloorPad(IWeightedFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                                      IFloorPadSpriteHandler spriteEffect, IValidator<int> pressValidator,
                                      IValidator<int> releaseValidator, IEventChannelSO pressed,
                                      IEventChannelSO released)
            : base(floorPadBehaviour, logger, gameObjectName, spriteEffect, pressValidator, releaseValidator, pressed,
                   released)
        {
            
        }

        public override void Release()
        {

        }

        public void PressInverse()
        {
            base.Release();
        }
    }
}