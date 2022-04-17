using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Commands;
using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovementScheduler : MonoBehaviour, ILoggableBehaviour
    {
        public string GameObjectName { get; private set; }

        private PlayerMovementBehaviour _playerMovement;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private PlayerMovementBehaviour _CloneMovement;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PlayerMovementChannel _PlayerMoveButtonPressed;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _TimelineEventChannel;

        public ILogger Logger => _Logger;

        private void Awake()
        {
            GameObjectName = gameObject.name;
            _playerMovement = GetComponent<PlayerMovementBehaviour>();
        }

        private void Start()
        {
            this.AddComponentInjectionByTagIfNotPresent(ref _CloneMovement, nameof(_CloneMovement), Constants.PlayerCloneTag);
        }

        private void OnMovement(Vector2 movementDirection)
        {
            var playerCommand = new MovementDelegateCommand(_playerMovement.CanMove, _playerMovement.Move,
                                                            movementDirection);

            if (!playerCommand.CanExecute(movementDirection))
            {
                _Logger.Log("Unable to execute player movement command.", GameObjectName);
                return;
            }

            playerCommand.Execute(movementDirection);
            _Logger.Log("Player movement command executed.", GameObjectName);

            ICommand cloneInverse = new MovementDelegateCommand(_CloneMovement.CanMove, _CloneMovement.Move,
                                                                -movementDirection);
            IRewindableCommand cloneCommand = new MovementDelegateCommand(_CloneMovement.CanMove, _CloneMovement.Move,
                                                                          movementDirection, 
                                                                          $"[{GameObjectName}] Move: {movementDirection}",
                                                                          cloneInverse);
            
            _TimelineEventChannel.Raise(cloneCommand);
        }

        private void OnEnable()
        {
            _PlayerMoveButtonPressed.Event += OnMovement;
        }

        private void OnDisable()
        {
            _PlayerMoveButtonPressed.Event -= OnMovement;
        }
    }
}
