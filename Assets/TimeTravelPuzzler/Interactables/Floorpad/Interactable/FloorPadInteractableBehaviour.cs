using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Timeline;
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
        private IInteractable<int> _interactable;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
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
        protected IEventChannelSO<IRewindableCommand> RecordableActionOccurred { get => _RecordableActionOccurred; }

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
    }
}