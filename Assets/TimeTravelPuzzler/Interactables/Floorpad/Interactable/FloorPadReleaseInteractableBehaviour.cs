using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadReleaseInteractableBehaviour : FloorPadInteractableBehaviourBase, IFloorPadReleaseInteractable
    {
        public override void Interact(int candidateWeight)
        {
            FloorPad.RemoveWeight(candidateWeight);
            IRewindableCommand releaseCommand = new NewFloorPadReleaseCommand(FloorPad, candidateWeight, Logger,
                                                                              GameObjectName);
            if (!releaseCommand.CanExecute())
            {
                Logger.Log("Release request rejected.", GameObjectName);
                return;
            }

            Logger.Log("Release approval received.", GameObjectName);
            releaseCommand.Execute();
            RecordableActionOccurred.Raise(releaseCommand);
            Interacted.Raise();
        }
    }
}