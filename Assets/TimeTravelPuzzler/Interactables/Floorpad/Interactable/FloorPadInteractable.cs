using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadInteractable : IInteractable<int>
    {
        private readonly string _gameObjectName;
        private readonly IFactory<IRewindableCommand, int> _commandFactory;
        private readonly ILogger _logger;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurred;

        public FloorPadInteractable(string gameObjectName, IFactory<IRewindableCommand, int> commandFactory,
                                       ILogger logger, IEventChannelSO<IRewindableCommand> recordableActionOccurred)
        {
            _gameObjectName = gameObjectName;
            _commandFactory = commandFactory;
            _logger = logger;
            _recordableActionOccurred = recordableActionOccurred;
        }

        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurred => _recordableActionOccurred;

        public void Interact(int parameter)
        {
            IRewindableCommand command = _commandFactory.Create(parameter);
            if (!command.CanExecute())
            {
                return;
            }

            command.Execute();
            _recordableActionOccurred.Raise(command);
        }
    }
}