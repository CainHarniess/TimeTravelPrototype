using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.Core;
using Osiris.Utilities.Logging;
using OUL = Osiris.Utilities.Logging;
using UnityEngine;
using Osiris.Utilities.Events;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class FloorPadInteractableBehaviourBase : MonoBehaviour, IInteractable<int>
    {
        private IWeightedFloorPad _floorPad;
        private string _gameObjectName;
        private FloorPadCommandFactoryBase _commandFactory;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private FloorPadEventChannelSO _Interacted;
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

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

        protected IWeightedFloorPad FloorPad { get => _floorPad; }
        protected OUL.ILogger Logger { get => _Logger; }
        protected IEventChannelSO Interacted { get => _Interacted; }
        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurred { get => _RecordableActionOccurred; }
        protected FloorPadCommandFactoryBase CommandFactory { get => _commandFactory; set => _commandFactory = value; }

        protected virtual void Awake()
        {
            _floorPad = GetComponent<IWeightedFloorPad>();
            _Logger.Configure();
        }

        public abstract void Interact(int candidateWeight);
    }
}