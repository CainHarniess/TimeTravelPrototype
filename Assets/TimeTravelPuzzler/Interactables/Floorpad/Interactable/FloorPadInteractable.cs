using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadInteractable : IInteractable<int>
    {
        private readonly IWeightedFloorPad _floorPad;
        private readonly FloorPadCommandBuildDirectorSO _commandBuildDirector;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurred;

        public FloorPadInteractable(IWeightedFloorPad floorPad, FloorPadCommandBuildDirectorSO commandBuildDirector,
                                    IEventChannelSO<IRewindableCommand> recordableActionOccurred)
        {
            _floorPad = floorPad;
            _commandBuildDirector = commandBuildDirector;
            _recordableActionOccurred = recordableActionOccurred;
        }

        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurred => _recordableActionOccurred;

        public void Interact(int interactorWeight)
        {
            IRewindableCommand command = _commandBuildDirector.Construct(_floorPad, interactorWeight);
            if (!command.CanExecute())
            {
                return;
            }

            command.Execute();
            _recordableActionOccurred.Raise(command);
        }
    }
}