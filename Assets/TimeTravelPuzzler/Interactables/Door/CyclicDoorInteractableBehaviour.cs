using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class CyclicDoorInteractableBehaviour : MonoBehaviour, IInteractable
    {
        private string _gameObjectName;
        private IDoor _doorBehaviour;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;


        private void Awake()
        {
            _doorBehaviour = GetComponent<IDoor>();
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
