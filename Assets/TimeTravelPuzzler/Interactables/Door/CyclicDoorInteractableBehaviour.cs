using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class CyclicDoorInteractableBehaviour : LoggableMonoBehaviour, IInteractable
    {
        private IDoor _doorBehaviour;

        protected override void Awake()
        {
            base.Awake();
            _doorBehaviour = GetComponent<IDoor>();
        }

        public void Interact()
        {
            if (_doorBehaviour.CanOpen())
            {
                Logger.Log("Door can open.", GameObjectName);
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
