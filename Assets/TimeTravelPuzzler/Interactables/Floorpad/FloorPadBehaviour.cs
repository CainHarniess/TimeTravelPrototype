using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadBehaviour : MonoBehaviour, IFloorPad
    {
        private PressSpriteEffect _spriteEffect;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _RequiredPressWeight;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IFloorPad _FloorPad;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private FloorPadEventChannelSO _Pressed;
        [SerializeField] private FloorPadEventChannelSO _Released;
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        protected UnityConsoleLogger Logger => _Logger;
        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; set => _FloorPad.CurrentPressWeight = value; }
        public bool IsPressed { get => _FloorPad.IsPressed; set => _FloorPad.IsPressed = value; }
        public IFloorPad FloorPad { get => _FloorPad; protected set => _FloorPad = value; }
        protected FloorPadEventChannelSO Pressed => _Pressed;
        protected FloorPadEventChannelSO Released => _Released;
        public TimelineActionChannel RecordableActionOccurred => _RecordableActionOccurred;
        public PressSpriteEffect SpriteEffect { get => _spriteEffect; protected set => _spriteEffect = value; }

        protected virtual void Awake()
        {
            _Logger.Configure();
            _spriteEffect = new PressSpriteEffect(GetComponent<SpriteRenderer>());
            _FloorPad = new FloorPad(this, _Logger, gameObject.name, _Pressed, _Released, _RecordableActionOccurred,
                                     _spriteEffect);
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