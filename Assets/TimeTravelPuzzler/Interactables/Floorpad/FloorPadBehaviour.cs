using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadBehaviour : MonoBehaviour, IFloorPad
    {
        [Header(InspectorHeaders.ControlVariables)]
        [Tooltip("Added at run time.")]
        [SerializeField] private IntReference _RequiredPressWeight;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IFloorPad _FloorPad;

        protected UnityConsoleLogger Logger => _Logger;
        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; }
        public bool IsPressed { get => _FloorPad.IsPressed; }
        public IFloorPad FloorPad { get => _FloorPad; protected set => _FloorPad = value; }

        protected virtual void Awake()
        {
            _Logger.Configure();
            _FloorPad = new FloorPad(this, _Logger, gameObject.name);
        }

        public virtual bool CanPress(int additionalWeight)
        {
            return _FloorPad.CanPress(additionalWeight);
        }

        public virtual void Press()
        {
            _FloorPad.Press();
        }

        public virtual bool CanRelease(int weightRemoved)
        {
            return _FloorPad.CanRelease(weightRemoved);
        }

        public virtual void Release()
        {
            _FloorPad.Release();
        }
    }
}