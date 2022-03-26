using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadInteractorBehaviour : MonoBehaviour
    {
        private IFloorPadInteractor _pressTriggerInteractor;
        private IFloorPadInteractor _releaseTriggerInteractor;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _Weight;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        private void Awake()
        {
            _Logger.Configure();
            _pressTriggerInteractor = new FloorPadPressTriggerInteractor(_Logger, gameObject.name);
            _releaseTriggerInteractor = new FloorPadReleaseTriggerInteractor(_Logger, gameObject.name);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(other.GetComponent<IFloorPad>() is IFloorPad floorPad))
            {
                _Logger.Log("Component implementing IFloorPad not found on candidate.", gameObject.name);
                return;
            }

            _pressTriggerInteractor.Interact(floorPad, _Weight.Value);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!(other.GetComponent<IFloorPad>() is IFloorPad floorPad))
            {
                _Logger.Log("Component implementing IFloorPad not found on candidate.", gameObject.name);
                return;
            }

            _releaseTriggerInteractor.Interact(floorPad, _Weight.Value);
        }
    }
}