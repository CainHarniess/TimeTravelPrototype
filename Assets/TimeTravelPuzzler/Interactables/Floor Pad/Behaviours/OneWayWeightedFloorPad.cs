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
        public OneWayWeightedFloorPad(IWeightedFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                                      IValidator<int> pressValidator, IValidator<int> releaseValidator,
                                      IEventChannelSO pressed, IEventChannelSO released)
            : base(floorPadBehaviour, logger, gameObjectName, pressValidator, releaseValidator, pressed, released)
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