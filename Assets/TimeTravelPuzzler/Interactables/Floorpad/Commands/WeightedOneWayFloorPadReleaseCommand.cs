using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class WeightedOneWayFloorPadReleaseCommand : WeightedFloorPadReleaseCommand
    {
        public WeightedOneWayFloorPadReleaseCommand(IFloorPad floorPad, int weightChange, IEventChannelSO pressedChannel,
                                                    IEventChannelSO releasedChannel, PressSpriteEffect spriteEffect,
                                                    IEventChannelSO<IRewindableCommand> recordableActionOccurredChannel,
                                                    ILogger logger, string gameObjectName)
            : base(floorPad, weightChange, pressedChannel, releasedChannel, spriteEffect, recordableActionOccurredChannel, logger, gameObjectName)
        {

        }

        public override string Description => "One-Way floor pad release";

        public override void Execute(object parameter = null)
        {
            if (!ShouldRecordExecution)
            {
                return;
            }
            RecordableActionOccurredChannel.Raise(this);
            ShouldRecordExecution = false;
        }

        public override bool CanExecute(object parameter = null)
        {
            FloorPad.CurrentPressWeight -= WeightChange;
            return true;
        }
    }
}