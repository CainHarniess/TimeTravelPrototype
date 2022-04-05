using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class EventInteractable : IInteractable<int>
    {
        private readonly string _gameObjectName;
        private readonly IFactory<IRewindableCommand, int> _commandFactory;
        private readonly ILogger _logger;
        private readonly IEventChannelSO _interacted;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurred;

        protected EventInteractable(string gameObjectName, IFactory<IRewindableCommand, int> commandFactory,
                                       ILogger logger, IEventChannelSO interacted,
                                       IEventChannelSO<IRewindableCommand> recordableActionOccurred)
        {
            _gameObjectName = gameObjectName;
            _commandFactory = commandFactory;
            _logger = logger;
            _interacted = interacted;
            _recordableActionOccurred = recordableActionOccurred;
        }

        protected IEventChannelSO Interacted => _interacted;
        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurred => _recordableActionOccurred;

        protected abstract string CanExecuteFailedMessage { get; }
        protected abstract string CanExecutePassedMessage { get; }

        public void Interact(int parameter)
        {
            IRewindableCommand command = _commandFactory.Create(parameter);
            if (!command.CanExecute())
            {
                _logger.Log(CanExecuteFailedMessage, _gameObjectName);
                return;
            }

            _logger.Log(CanExecutePassedMessage, _gameObjectName);

            command.Execute();
            _recordableActionOccurred.Raise(command);
            _interacted.Raise();
        }
    }
}