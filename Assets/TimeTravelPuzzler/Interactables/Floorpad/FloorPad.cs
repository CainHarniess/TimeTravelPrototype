using Osiris.EditorCustomisation;
using O = Osiris.Utilities.Logging;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [Serializable]
    public class FloorPad : IFloorPad
    {
        private readonly string _gameObjectName;
        private readonly O::ILogger _Logger;
        private readonly IFloorPad _floorPadBehaviour;
        
        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        public FloorPad(IFloorPad floorPadBehaviour, O::ILogger logger)
        {
            _floorPadBehaviour = floorPadBehaviour;
            _Logger = logger;
            _CurrentPressWeight = 0;
            _IsPressed = false;
        }

        protected string GameObjectName => _gameObjectName;
        protected O::ILogger Logger => _Logger;
        public int CurrentPressWeight { get => _CurrentPressWeight; protected set => _CurrentPressWeight = value; }
        public bool IsPressed => _IsPressed;
        public int RequiredPressWeight => _floorPadBehaviour.RequiredPressWeight;

        public virtual bool CanPress(int additionalWeight)
        {
            _CurrentPressWeight += additionalWeight;

            if (_IsPressed)
            {
                _Logger.Log("Floor pad is already pressed.", _gameObjectName);
                return false;
            }

            if (_CurrentPressWeight < RequiredPressWeight)
            {
                _Logger.Log("Insufficient weight on floor pad.", _gameObjectName);
                return false;
            }

            _Logger.Log("Floor pad press approved.", _gameObjectName);
            return true;
        }

        public virtual void Press()
        {
            if (_IsPressed)
            {
                _Logger.Log("Pressing floor pad that is already pressed.", _gameObjectName, O::LogLevel.Warning);
                return;
            }
            _IsPressed = true;
        }

        public virtual bool CanRelease(int weightRemoved)
        {
            _CurrentPressWeight -= weightRemoved;

            if (!_IsPressed)
            {
                _Logger.Log("Floor pad is already released.", _gameObjectName);
                return false;
            }

            if (_CurrentPressWeight >= RequiredPressWeight)
            {
                _Logger.Log("Floor pad weight is above still limit.", _gameObjectName);
                return false;
            }

            _Logger.Log("Floor pad release approved.", _gameObjectName);
            return true;
        }

        public virtual void Release()
        {
            if (!_IsPressed)
            {
                _Logger.Log("Releasing floor pad that is already released.", _gameObjectName, O::LogLevel.Warning);
                return;
            }
            _IsPressed = false;
        }
    }
}