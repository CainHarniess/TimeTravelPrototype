using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadInteractableBehaviour : MonoBehaviour, IInteractable<int>
    {
        private IWeightedFloorPad _floorPad;
        private IInteractable<int> _interactable;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private CommandBuildDirectorSO _CommandDirector;
        [SerializeField] private InteractableBuildDirectorSO _InteractableDirector;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _TimelineEventOccurred;

        protected IWeightedFloorPad FloorPad { get => _floorPad; }
        protected CommandBuildDirectorSO CommandDirector => _CommandDirector;
        protected IEventChannelSO<IRewindableCommand> TimelineEventOccurred { get => _TimelineEventOccurred; }

        private void Awake()
        {
            _floorPad = GetComponent<IWeightedFloorPad>();
            _interactable = _InteractableDirector.Construct(FloorPad, CommandDirector, TimelineEventOccurred);
        }

        public void Interact(int candidateWeight)
        {
            _interactable.Interact(candidateWeight);
        }
    }

}