using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadInteractableBehaviour : MonoBehaviour, IInteractable<int>
    {
        private IWeightedFloorPad _floorPad;
        private string _gameObjectName;
        private IFactory<IRewindableCommand, int> _commandFactory;
        private IInteractable<int> _interactable;

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
        protected IFactory<IRewindableCommand, int> CommandFactory { get => _commandFactory; set => _commandFactory = value; }

        private void Awake()
        {
            _floorPad = GetComponent<IWeightedFloorPad>();
            _Logger.Configure();
            _interactable = GetInteractable();
        }

        public void Interact(int candidateWeight)
        {
            _interactable.Interact(candidateWeight);
        }

        protected abstract IInteractable<int> GetInteractable();

        protected abstract IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad);
    }
}