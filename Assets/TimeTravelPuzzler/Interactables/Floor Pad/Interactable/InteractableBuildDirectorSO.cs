using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.InteractableBuildDirectorFileName, menuName = AssetMenu.InteractableBuildDirectorPath)]
    public class InteractableBuildDirectorSO : ScriptableObject
    {
        [SerializeField] InteractableBuilderSO _builder;
        public IInteractable<int> Construct(IWeightedFloorPad floorPad, CommandBuildDirectorSO commandBuildDirector,
                                            IEventChannelSO<IRewindableCommand> timelineEventChannel)
        {
            return _builder.WithFloorPad(floorPad).WithDirector(commandBuildDirector)
                           .WithTimelineEventChannel(timelineEventChannel).Build();
        }
    }

}