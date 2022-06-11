using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadBehaviour : LoggableMonoBehaviour, IWeightedFloorPad, IInjectableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private IntReference _RequiredPressWeight;
        [SerializeField] private FloorPadBuildDirectorSO _floorPadBuildDirector;
        private IFloorPadBehaviourHandler[] _BehaviourHandlers;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private IWeightedFloorPad _FloorPad;
        
        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private FloorPadEventChannelSO _Pressed;
        [SerializeField] private FloorPadEventChannelSO _Released;

        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; }
        public bool IsPressed { get => _FloorPad.IsPressed; }
        public IWeightedFloorPad FloorPad { get => _FloorPad; }

        protected override void Awake()
        {
            base.Awake();
            _FloorPad = _floorPadBuildDirector.Construct(this, Logger, GameObjectName, _Pressed, _Released);
            _BehaviourHandlers = GetComponents<IFloorPadBehaviourHandler>();
        }

        public void AddWeight(int weightToAdd)
        {
            _FloorPad.AddWeight(weightToAdd);
        }

        public void RemoveWeight(int weightToRemove)
        {
            _FloorPad.RemoveWeight(weightToRemove);
        }

        public virtual bool CanPress()
        {
            return _FloorPad.CanPress();
        }

        public virtual void Press()
        {
            _FloorPad.Press();
            HandlePress();
        }

        public virtual bool CanRelease()
        {
            return _FloorPad.CanRelease();
        }

        public virtual void Release()
        {
            _FloorPad.Release();
            HandleRelease();
        }

        private void HandlePress()
        {
            foreach (IFloorPadBehaviourHandler handler in _BehaviourHandlers)
            {
                handler.OnPress();
            }
        }

        protected void HandleRelease()
        {
            foreach (IFloorPadBehaviourHandler handler in _BehaviourHandlers)
            {
                handler.OnRelease();
            }
        }
    }
}