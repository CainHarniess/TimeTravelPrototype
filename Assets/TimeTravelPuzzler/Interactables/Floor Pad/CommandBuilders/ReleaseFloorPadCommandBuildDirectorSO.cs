using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [CreateAssetMenu(fileName = AssetMenu.ReleaseBuildDirectorFileName, menuName = AssetMenu.ReleaseBuildDirectorPath)]
    public class ReleaseFloorPadCommandBuildDirectorSO : FloorPadCommandBuildDirectorSO
    {
        protected override ICommand BuildInverse(IWeightedFloorPad floorPad, int candidateWeight)
        {
            return CommandBuilder.WithCandidateWeight(candidateWeight).WithCanExecute(floorPad.CanPress)
                                              .WithExecute(floorPad.Press).WithAdjustWeight(floorPad.AddWeight)
                                              .WithCommandDescription("Floor pad release inverse").Build();
        }

        protected override IRewindableCommand BuildCommand(IWeightedFloorPad floorPad, int candidateWeight, ICommand inverse)
        {
            return CommandBuilder.WithCandidateWeight(candidateWeight).WithCanExecute(floorPad.CanRelease)
                                 .WithExecute(floorPad.Release).WithAdjustWeight(floorPad.RemoveWeight)
                                 .WithCommandDescription("Floor pad release").WithInverse(inverse).Build();
        }
    }

}
