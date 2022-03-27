using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class DoorInteractableBehaviour : MonoBehaviour, IInteractable
    {
        private IDoor _DoorBehaviour;

        private void Awake()
        {
            _DoorBehaviour = GetComponent<IDoor>();
        }

        public void Interact()
        {
            if (_DoorBehaviour.CanOpen())
            {
                _DoorBehaviour.Open();
                return;
            }

            if (_DoorBehaviour.CanClose())
            {
                _DoorBehaviour.Close();
                return;
            }
        }
    }
}
