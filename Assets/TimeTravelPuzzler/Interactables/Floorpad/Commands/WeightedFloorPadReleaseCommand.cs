using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class WeightedFloorPadReleaseCommand : WeightedFloorPadCommand
    {
        public WeightedFloorPadReleaseCommand(IFloorPad floorPad, int weightChange, IEventChannelSO pressedChannel,
                                              IEventChannelSO releasedChannel, PressSpriteEffect spriteEffect,
                                              IEventChannelSO<IRewindableCommand> recordableActionOccurredChannel,
                                              ILogger logger,string gameObjectName)
            : base(floorPad, weightChange, pressedChannel, releasedChannel, spriteEffect,
                   recordableActionOccurredChannel, logger, gameObjectName)
        {
            
        }
        
        public override ICommand Inverse
        {
            get
            {
                return new WeightedFloorPadPressCommand(FloorPad, WeightChange, PressedChannel, ReleasedChannel,
                                                        SpriteEffect, RecordableActionOccurredChannel, Logger,
                                                        GameObjectName)
                {
                    ShouldRecordExecution = false
                };
            }
        }

        public override string Description => "Floor pad release";

        public override bool CanExecute(object parameter = null)
        {
            FloorPad.CurrentPressWeight -= WeightChange;

            if (!FloorPad.IsPressed)
            {
                Logger.Log("Floor pad is already released.", GameObjectName);
                return false;
            }

            if (FloorPad.CurrentPressWeight >= FloorPad.RequiredPressWeight)
            {
                Logger.Log("Floor pad weight is still above limit.", GameObjectName);
                return false;
            }

            Logger.Log("Floor pad release approved.", GameObjectName);
            return true;
        }

        public override void Execute(object parameter = null)
        {
            ReleasedChannel.Raise();
            FloorPad.IsPressed = false;
            SpriteEffect.Lighten();

            if (!ShouldRecordExecution)
            {
                return;
            }
            RecordableActionOccurredChannel.Raise(this);
            ShouldRecordExecution = false;
        }
    }
}