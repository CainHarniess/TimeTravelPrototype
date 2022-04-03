using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class WeightedFloorPadPressCommand : WeightedFloorPadCommand
    {
        public WeightedFloorPadPressCommand(IFloorPad floorPad, int weightChange, IEventChannelSO pressedChannel,
                                            IEventChannelSO releasedChannel, PressSpriteEffect spriteEffect,
                                            IEventChannelSO<IRewindableCommand> recordableActionOccurredChannel,
                                            ILogger logger, string gameObjectName)
            : base(floorPad, weightChange, pressedChannel, releasedChannel, spriteEffect,
                   recordableActionOccurredChannel, logger, gameObjectName)
        {

        }

        public override string Description => "Floor pad press";
        public override ICommand Inverse
        {
            get
            {
                return new WeightedFloorPadReleaseCommand(FloorPad, WeightChange, PressedChannel, ReleasedChannel,
                                                          SpriteEffect, RecordableActionOccurredChannel, Logger,
                                                          GameObjectName)
                {
                    ShouldRecordExecution = false
                };
            }
        }

        public override bool CanExecute(object parameter = null)
        {
            FloorPad.CurrentPressWeight += WeightChange;

            if (FloorPad.IsPressed)
            {
                Logger.Log("Floor pad is already pressed.", GameObjectName);
                return false;
            }

            if (FloorPad.CurrentPressWeight < FloorPad.RequiredPressWeight)
            {
                Logger.Log("Insufficient weight on floor pad.", GameObjectName);
                return false;
            }

            Logger.Log("Floor pad press approved.", GameObjectName);
            return true;
        }

        public override void Execute(object parameter = null)
        {
            PressedChannel.Raise();
            FloorPad.IsPressed = true;
            SpriteEffect.Darken();

            if (!ShouldRecordExecution)
            {
                return;
            }
            RecordableActionOccurredChannel.Raise(this);
            ShouldRecordExecution = false;
        }
    }
}