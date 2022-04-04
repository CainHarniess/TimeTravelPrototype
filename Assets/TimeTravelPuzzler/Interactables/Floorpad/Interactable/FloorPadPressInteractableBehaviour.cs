using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadPressInteractableBehaviour : FloorPadInteractableBehaviourBase, IFloorPadPressInteractable
    {
        public override void Interact(int candidateWeight)
        {
            FloorPad.AddWeight(candidateWeight);
            IRewindableCommand pressCommand = new NewFloorPadPressCommand(FloorPad, candidateWeight, Logger,
                                                                          GameObjectName);
            if (!pressCommand.CanExecute())
            {
                Logger.Log("Press request rejected.", GameObjectName);
                return;
            }

            Logger.Log("Press approval received.", GameObjectName);
            pressCommand.Execute();
            RecordableActionOccurred.Raise(pressCommand);
            Interacted.Raise();
        }
    }
}