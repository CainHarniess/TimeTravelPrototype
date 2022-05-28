using Osiris.EditorCustomisation;
using Osiris.Testing;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{

    public class FloorPadBehaviour : MonoBehaviour, IWeightedFloorPad
    {
        private string _gameObjectName;
        private IFloorPadSpriteHandler _spriteHandler;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private IntReference _RequiredPressWeight;
        [SerializeField] private FloorPadBuildDirectorSO _floorPadBuildDirector;
        [SerializeField] private FloorpadSfxPlayer _SfxPlayer;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IWeightedFloorPad _FloorPad;
        
        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private FloorPadEventChannelSO _Pressed;
        [SerializeField] private FloorPadEventChannelSO _Released;

        protected string GameObjectName
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
        protected IFloorPadSpriteHandler SpriteHandler { get => _spriteHandler; }
        protected ILogger Logger { get => _Logger; }
        protected IEventChannelSO Pressed { get => _Pressed; }
        protected IEventChannelSO Released { get => _Released; }
        
        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; }
        public bool IsPressed { get => _FloorPad.IsPressed; }
        public IWeightedFloorPad FloorPad { get => _FloorPad; }
        protected FloorpadSfxPlayer SfxPlayer => _SfxPlayer;

        protected virtual void Awake()
        {
            _Logger.Configure();
            _spriteHandler = new PrimitiveFloorPadSpriteHandler(new SpriteRendererProxy(GetComponent<SpriteRenderer>()));
            _FloorPad = _floorPadBuildDirector.Construct(this, _Logger, GameObjectName, SpriteHandler, _Pressed,
                                                         _Released);
            if (_SfxPlayer == null)
            {
                _SfxPlayer = GetComponent<FloorpadSfxPlayer>();
            }
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
            SpriteHandler.OnPress();
            _SfxPlayer.OnPress();
        }

        public virtual bool CanRelease()
        {
            return _FloorPad.CanRelease();
        }

        public virtual void Release()
        {
            _FloorPad.Release();
            SpriteHandler.OnRelease();
            _SfxPlayer.OnRelease();
        }
    }
}