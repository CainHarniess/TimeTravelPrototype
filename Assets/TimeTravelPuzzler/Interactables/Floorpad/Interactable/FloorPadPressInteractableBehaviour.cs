using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadPressInteractableBehaviour : FloorPadInteractableBehaviourBase, IFloorPadPressInteractable
    {
        protected override void Awake()
        {
            base.Awake();
            CommandFactory = new FloorPadPressCommandFactory(FloorPad);
        }

        public override void Interact(int candidateWeight)
        {
            IRewindableCommand pressCommand = CommandFactory.Create(candidateWeight);
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