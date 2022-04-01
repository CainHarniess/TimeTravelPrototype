using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPad : FloorPad
    {
        public OneWayFloorPad(IFloorPad floorPadBehaviour, ILogger logger, string gameObjectName,
                              IEventChannelSO pressChannel, IEventChannelSO releaseChannel,
                              IEventChannelSO<IRewindableCommand> recordableEventOccurredChannel)
            : base(floorPadBehaviour, logger, gameObjectName, pressChannel, releaseChannel,
                   recordableEventOccurredChannel)
        {

        }

        public OneWayFloorPad(IFloorPad floorPadBehaviour, ILogger logger, string gameObjectName,
                              IEventChannelSO pressChannel, IEventChannelSO releaseChannel,
                              IEventChannelSO<IRewindableCommand> recordableEventOccurredChannel,
                              PressSpriteEffect spriteEffect)
            : base(floorPadBehaviour, logger, gameObjectName, pressChannel, releaseChannel,
                   recordableEventOccurredChannel, spriteEffect)
        {

        }

        public override bool CanRelease(int weightRemoved)
        {
            TempCommand = new WeightedOneWayFloorPadReleaseCommand(this, weightRemoved, PressedChannel, ReleasedChannel,
                                                                   SpriteEffect, RecordableActionOccurredChannel, Logger,
                                                                   GameObjectName);
            return TempCommand.CanExecute();
        }

        public override void Release()
        {
            Logger.Log("One-way floor pads may not be released.", GameObjectName, LogLevel.Error);
            base.Release();
        }
    }
}