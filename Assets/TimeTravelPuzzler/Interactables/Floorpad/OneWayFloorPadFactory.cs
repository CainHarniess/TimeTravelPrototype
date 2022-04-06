using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation;
using Osiris.Utilities.Events;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class OneWayFloorPadFactory : FloorPadFactory
    {
        public OneWayFloorPadFactory(ILogger logger, string gameObjectName, IFloorPadSpriteHandler spriteHandler,
                               IEventChannelSO pressed, IEventChannelSO released)
            : base(logger, gameObjectName, spriteHandler, pressed, released)
        {

        }
        public override IWeightedFloorPad Create(IFloorPad floorPadBehaviour)
        {
            var pressValidator = new FloorPadPressValidator(floorPadBehaviour, Logger, GameObjectName);
            var releaseValidator = new FloorPadReleaseValidator(floorPadBehaviour, Logger, GameObjectName);
            return new WeightedFloorPad(floorPadBehaviour, Logger, GameObjectName, SpriteHandler, pressValidator,
                                        releaseValidator, Pressed, Released);
        }
    }
}