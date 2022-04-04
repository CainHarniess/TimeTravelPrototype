using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Events;
using System;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [Serializable]
    public class WeightedFloorPad : IWeightedFloorPad
    {
        private readonly string _gameObjectName;
        private readonly OUL::ILogger _Logger;
        private readonly IFloorPad _floorPadBehaviour;
        private readonly IEventChannelSO _pressed;
        private readonly IEventChannelSO _released;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurred;
        private readonly PressSpriteEffect _spriteEffect;
        
        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        public WeightedFloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                        PressSpriteEffect spriteEffect)
        {
            _floorPadBehaviour = floorPadBehaviour;
            _Logger = logger;
            _gameObjectName = gameObjectName;
            _spriteEffect = spriteEffect;
        }

        #region Properties
        protected string GameObjectName => _gameObjectName;
        protected OUL::ILogger Logger => _Logger;
        public int CurrentPressWeight
        {
            get => _CurrentPressWeight;
            set
            {
                _CurrentPressWeight = value;
                if (_CurrentPressWeight < 0)
                {
                    _Logger.Log("Current floor pad weight may not be negative.", GameObjectName, OUL.LogLevel.Error);
                    throw new ArgumentException("Current floor pad weight may not be negative.", nameof(CurrentPressWeight));
                }
            }
        }
        public bool IsPressed { get => _IsPressed; set => _IsPressed = value; }

        public int RequiredPressWeight => _floorPadBehaviour.RequiredPressWeight;
        protected PressSpriteEffect SpriteEffect => _spriteEffect;
        public IEventChannelSO PressedChannel => _pressed;
        public IEventChannelSO ReleasedChannel => _released;
        public IEventChannelSO<IRewindableCommand> RecordableActionOccurredChannel => _recordableActionOccurred;
        #endregion

        /// <summary>
        /// Validates the argument and adds it onto the weight currently on the floor pad. Does nothing if the argument is invalid.
        /// </summary>
        /// <param name="weightToRemove">The amount that should be added. This should be a positive number.</param>
        public void AddWeight(int weightToAdd)
        {
            if (!IsAdditionalWeightValid(weightToAdd))
            {
                return;
            }
            _CurrentPressWeight += weightToAdd;
        }

        /// <summary>
        /// Validates the argument and removes it from the weight currently on the floor pad. Does nothing if the argument is invalid.
        /// </summary>
        /// <param name="weightToRemove">The amount that should be deducted. This should be a positive number.</param>
        public void RemoveWeight(int weightToRemove)
        {
            if (!IsRemovalWeightValid(weightToRemove))
            {
                return;
            }
            _CurrentPressWeight -= weightToRemove;
        }

        public virtual bool CanPress()
        {
            if (IsPressed)
            {
                Logger.Log("Floor pad is already pressed.", GameObjectName);
                return false;
            }

            if (CurrentPressWeight < RequiredPressWeight)
            {
                Logger.Log("Insufficient weight on floor pad.", GameObjectName);
                return false;
            }

            Logger.Log("Floor pad press approved.", GameObjectName);
            return true;
        }

        public virtual void Press()
        {
            IsPressed = true;
            SpriteEffect.Darken();
        }

        public virtual bool CanRelease()
        {
            if (!IsPressed)
            {
                Logger.Log("Floor pad is already released.", GameObjectName);
                return false;
            }

            if (CurrentPressWeight >= RequiredPressWeight)
            {
                Logger.Log("Floor pad weight is still above limit.", GameObjectName);
                return false;
            }

            Logger.Log("Floor pad release approved.", GameObjectName);
            return true;
        }

        public virtual void Release()
        {
            IsPressed = false;
            SpriteEffect.Lighten();
        }

        #region Refactor To Separate Validator Class
        private bool IsAdditionalWeightValid(int weightToAdd)
        {
            if (weightToAdd < 0)
            {
                Logger.Log("Weight to add may not be negative.", GameObjectName, OUL.LogLevel.Error);
                return false;
            }
            return true;
        }

        private bool IsRemovalWeightValid(int weightToRemove)
        {
            if (weightToRemove < 0)
            {
                Logger.Log("Weight to remove may not be negative.", GameObjectName, OUL.LogLevel.Error);
                return false;
            }
            
            if (_CurrentPressWeight - weightToRemove < 0)
            {
                Logger.Log("Remaining weight on floor pad after removal may not be negative.", GameObjectName,
                           OUL.LogLevel.Error);
                return false;
            }
            return true;
        }
        #endregion
    }
}