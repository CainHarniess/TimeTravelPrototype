using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class WeightedFloorPadBehaviour : MonoBehaviour, IWeightedFloorPad
    {
        private string _gameObjectName;
        private PrimitiveFloorPadSpriteHandler _spriteEffect;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _RequiredPressWeight;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IWeightedFloorPad _FloorPad;

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
        public PrimitiveFloorPadSpriteHandler SpriteEffect { get => _spriteEffect; }
        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; }
        public bool IsPressed { get => _FloorPad.IsPressed; }

        protected virtual void Awake()
        {
            _Logger.Configure();
            _spriteEffect = new PrimitiveFloorPadSpriteHandler(GetComponent<SpriteRenderer>());

            var pressValidator = new FloorPadPressValidator(this, _Logger, GameObjectName);
            var releaseValidator = new FloorPadReleaseValidator(this, _Logger, GameObjectName);

            _FloorPad = new WeightedFloorPad(this, _Logger, GameObjectName, _spriteEffect, pressValidator, releaseValidator);
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
        }

        public virtual bool CanRelease()
        {
            return _FloorPad.CanRelease();
        }

        public virtual void Release()
        {
            _FloorPad.Release();
        }
    }
}