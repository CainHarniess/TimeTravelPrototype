using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class WeightedFloorPadBehaviour : MonoBehaviour, IWeightedFloorPad
    {
        private string _gameObjectName;
        private PressSpriteEffect _spriteEffect;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _RequiredPressWeight;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IWeightedFloorPad _FloorPad;

        protected UnityConsoleLogger Logger => _Logger;
        public int RequiredPressWeight => _RequiredPressWeight.Value;
        public int CurrentPressWeight { get => _FloorPad.CurrentPressWeight; set => _FloorPad.CurrentPressWeight = value; }
        public bool IsPressed { get => _FloorPad.IsPressed; set => _FloorPad.IsPressed = value; }
        public IWeightedFloorPad FloorPad { get => _FloorPad; protected set => _FloorPad = value; }
        public PressSpriteEffect SpriteEffect { get => _spriteEffect; protected set => _spriteEffect = value; }
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

        protected virtual void Awake()
        {
            _Logger.Configure();
            _spriteEffect = new PressSpriteEffect(GetComponent<SpriteRenderer>());
            _FloorPad = new WeightedFloorPad(this, _Logger, GameObjectName, _spriteEffect);
        }

        /// <summary>
        /// Forwards the execution on to the nested CLR object.
        /// </summary>
        /// <param name="weightToRemove">The amount that should be added. This should be a positive number.</param>
        public void AddWeight(int weightToAdd)
        {
            _FloorPad.AddWeight(weightToAdd);
        }

        /// <summary>
        /// Forwards the execution on to the nested CLR object.
        /// </summary>
        /// <param name="weightToRemove">The amount that should be deducted. This should be a positive number.</param>
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