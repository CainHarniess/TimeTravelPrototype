using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Validation;
using System;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [Serializable]
    public class WeightedFloorPad : IWeightedFloorPad
    {
        private readonly string _gameObjectName;
        private readonly OUL::ILogger _Logger;
        private readonly IFloorPad _floorPadBehaviour;
        private readonly ISpriteHandler _spriteEffect;
        private readonly IValidator<int> _pressValidator;
        private readonly IValidator<int> _releaseValidator;

        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        public WeightedFloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                                ISpriteHandler spriteEffect, IValidator<int> pressValidator,
                                IValidator<int> releaseValidator)
        {
            _floorPadBehaviour = floorPadBehaviour;
            _Logger = logger;
            _gameObjectName = gameObjectName;
            _spriteEffect = spriteEffect;
            _pressValidator = pressValidator;
            _releaseValidator = releaseValidator;
        }

        public string GameObjectName => _gameObjectName;
        public int CurrentPressWeight => _CurrentPressWeight;
        public bool IsPressed { get => _IsPressed; }
        public int RequiredPressWeight => _floorPadBehaviour.RequiredPressWeight;

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

        public void Release()
        {
            _IsPressed = false;
            _spriteEffect.OnRelease();
        }
    }
}