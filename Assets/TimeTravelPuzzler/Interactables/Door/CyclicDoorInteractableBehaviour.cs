using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Doors.Commands;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class CyclicDoorInteractableBehaviour : MonoBehaviour, IInteractable
    {
        private string _gameObjectName;
        private IDoor _doorBehaviour;
        private IFactory<IRewindableCommand, DoorInteractionType> _commandFactory;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        private void Awake()
        {
            _doorBehaviour = GetComponent<IDoor>();
            _commandFactory = new DoorCommandFactory(_doorBehaviour);
        }

        private string GameObjectName
        {
            get
            {
                if (_gameObjectName == null)
                {
                    _gameObjectName = gameObject.name;
                }
                return _gameObjectName;
            }
        }

        public void Interact()
        {
            if (_doorBehaviour.CanOpen())
            {
                _doorBehaviour.Open();
            }
            else if (_doorBehaviour.CanClose())
            {
                _doorBehaviour.Close();
            }
            else
            {
                throw new InvalidOperationException("Door can neither open nor close.");
            }
        }
    }
}
