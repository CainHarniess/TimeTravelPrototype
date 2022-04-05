using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadReleaseInteractableBehaviour : FloorPadInteractableBehaviourBase, IFloorPadReleaseInteractable
    {
        protected override void Awake()
        {
            base.Awake();
            CommandFactory = new FloorPadReleaseCommandFactory(FloorPad);
        }

        public override void Interact(int candidateWeight)
        {
            IRewindableCommand releaseCommand = CommandFactory.Create(candidateWeight);
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