using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Validation;
using System;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{

    [Serializable]
    public class WeightedFloorPad : IWeightedFloorPad
    {
        private readonly string _gameObjectName;
        private readonly ILogger _Logger;
        private readonly IWeightedFloorPad _floorPadBehaviour;
        private readonly IFloorPadSpriteHandler _spriteEffect;
        private readonly IValidator<int> _pressValidator;
        private readonly IValidator<int> _releaseValidator;
        private readonly IEventChannelSO _pressed;
        private readonly IEventChannelSO _released;

        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        public WeightedFloorPad(IWeightedFloorPad floorPadBehaviour, ILogger logger, string gameObjectName,
                                IFloorPadSpriteHandler spriteEffect, IValidator<int> pressValidator,
                                IValidator<int> releaseValidator, IEventChannelSO pressed, IEventChannelSO released)
        {
            _floorPadBehaviour = floorPadBehaviour;
            _Logger = logger;
            _gameObjectName = gameObjectName;
            _spriteEffect = spriteEffect;
            _pressValidator = pressValidator;
            _releaseValidator = releaseValidator;
            _pressed = pressed;
            _released = released;
        }

        public string GameObjectName => _gameObjectName;
        public int CurrentPressWeight => _CurrentPressWeight;
        public bool IsPressed { get => _IsPressed; protected set => _IsPressed = value; }
        public int RequiredPressWeight => _floorPadBehaviour.RequiredPressWeight;
        protected IFloorPadSpriteHandler SpriteEffect => _spriteEffect;

        public void AddWeight(int weightToAdd)
        {
            if (!_pressValidator.IsValid(weightToAdd))
            {
                return;
            }
            _CurrentPressWeight += weightToAdd;
        }

        public void RemoveWeight(int weightToRemove)
        {
            if (!_releaseValidator.IsValid(weightToRemove))
            {
                return;
            }
            _CurrentPressWeight -= weightToRemove;
        }

        public bool CanPress()
        {
            if (IsPressed)
            {
                _Logger.Log("Floor pad is already pressed.", GameObjectName);
                return false;
            }

            if (CurrentPressWeight < RequiredPressWeight)
            {
                _Logger.Log("Insufficient weight on floor pad.", GameObjectName);
                return false;
            }

            _Logger.Log("Floor pad press approved.", GameObjectName);
            return true;
        }

        public void Press()
        {
            _IsPressed = true;
            _spriteEffect.OnPress();
            _pressed.Raise();
        }

        public bool CanRelease()
        {
            if (!IsPressed)
            {
                _Logger.Log("Floor pad is already released.", GameObjectName);
                return false;
            }

            if (CurrentPressWeight >= RequiredPressWeight)
            {
                _Logger.Log("Floor pad weight is still above limit.", GameObjectName);
                return false;
            }

            _Logger.Log("Floor pad release approved.", GameObjectName);
            return true;
        }

        public virtual void Release()
        {
            _IsPressed = false;
            _spriteEffect.OnRelease();
            _released.Raise();
        }
    }
}