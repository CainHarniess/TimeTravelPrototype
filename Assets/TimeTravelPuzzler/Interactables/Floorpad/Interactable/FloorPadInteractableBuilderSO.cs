using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.InteractableBuilderFileName, menuName = AssetMenu.InteractableBuilderPath)]
    public class FloorPadInteractableBuilderSO : ScriptableObject
    {
        public IWeightedFloorPad FloorPad { get; private set; }
        public FloorPadInteractableBuilderSO WithFloorPad(IWeightedFloorPad floorPad)
        {
            FloorPad = floorPad;
            return this;
        }

        public FloorPadCommandBuildDirectorSO CommandBuildDirector { get; private set; }
        public FloorPadInteractableBuilderSO WithDirector(FloorPadCommandBuildDirectorSO commandBuildDirector)
        {
            CommandBuildDirector = commandBuildDirector;
            return this;
        }

        public IEventChannelSO<IRewindableCommand> TimelineEventChannel { get; private set; }
        public FloorPadInteractableBuilderSO WithTimelineEventChannel(IEventChannelSO<IRewindableCommand> timelineEventChannel)
        {
            TimelineEventChannel = timelineEventChannel;
            return this;
        }
        
        public IInteractable<int> Build()
        {
            return new FloorPadInteractable(FloorPad, CommandBuildDirector, TimelineEventChannel);
        }
    }

}