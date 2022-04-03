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
    public class FloorPad : IFloorPad
    {
        private readonly string _gameObjectName;
        private readonly OUL::ILogger _Logger;
        private readonly IFloorPad _floorPadBehaviour;
        private readonly IEventChannelSO _pressed;
        private readonly IEventChannelSO _released;
        private readonly IEventChannelSO<IRewindableCommand> _recordableActionOccurred;
        private readonly PressSpriteEffect _spriteEffect;
        
        private IRewindableCommand _tempCommand;
        
        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        #region Constructors
        private FloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger)
        {
            _floorPadBehaviour = floorPadBehaviour;
            _Logger = logger;
            _CurrentPressWeight = 0;
            _IsPressed = false;
        }
        private FloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName)
            : this(floorPadBehaviour, logger)
        {
            _gameObjectName = gameObjectName;
        }

        private FloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                        IEventChannelSO pressChannel, IEventChannelSO releaseChannel)
            : this(floorPadBehaviour, logger, gameObjectName)
        {
            _pressed = pressChannel;
            _released = releaseChannel;
        }

        public FloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                        IEventChannelSO pressChannel, IEventChannelSO releaseChannel,
                        IEventChannelSO<IRewindableCommand> recordableEventOccurredChannel)
            : this(floorPadBehaviour, logger, gameObjectName, pressChannel, releaseChannel)
        {
            _recordableActionOccurred = recordableEventOccurredChannel;
        }

        public FloorPad(IFloorPad floorPadBehaviour, OUL::ILogger logger, string gameObjectName,
                        IEventChannelSO pressChannel, IEventChannelSO releaseChannel,
                        IEventChannelSO<IRewindableCommand> recordableEventOccurredChannel,
                        PressSpriteEffect spriteEffect)
            : this(floorPadBehaviour, logger, gameObjectName, pressChannel, releaseChannel)
        {
            _recordableActionOccurred = recordableEventOccurredChannel;
            _spriteEffect = spriteEffect;
        }
        #endregion

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
        protected IRewindableCommand TempCommand { get => _tempCommand; set => _tempCommand = value; }
        public IEventChannelSO PressedChannel => _pressed;
        public IEventChannelSO ReleasedChannel => _released;
        public IEventChannelSO<IRewindableCommand> RecordableActionOccurredChannel => _recordableActionOccurred;

        #endregion

        public virtual bool CanPress(int additionalWeight)
        {
            _tempCommand = new WeightedFloorPadPressCommand(this, additionalWeight, _pressed, _released, _spriteEffect,
                                                            _recordableActionOccurred, Logger, GameObjectName);
            return _tempCommand.CanExecute();
        }

        public virtual void Press()
        {
            if (_IsPressed)
            {
                _Logger.Log("Pressing floor pad that is already pressed.", _gameObjectName, OUL::LogLevel.Error);
                return;
            }
            _tempCommand.Execute();
        }

        public virtual bool CanRelease(int weightRemoved)
        {
            _tempCommand = new WeightedFloorPadReleaseCommand(this, weightRemoved, _pressed, _released, _spriteEffect,
                                                              _recordableActionOccurred, Logger, GameObjectName);
            return _tempCommand.CanExecute();
        }

        public virtual void Release()
        {
            if (!_IsPressed)
            {
                _Logger.Log("Releasing floor pad that is already released.", _gameObjectName, OUL::LogLevel.Warning);
                return;
            }
            _tempCommand.Execute();
        }
    }
}