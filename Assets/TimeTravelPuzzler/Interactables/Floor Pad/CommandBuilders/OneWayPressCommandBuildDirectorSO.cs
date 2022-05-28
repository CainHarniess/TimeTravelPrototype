using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Commands;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.OneWayPressCommandDirectorFileName, menuName = AssetMenu.OneWayPressCommandDirectorPath)]
    public class OneWayPressCommandBuildDirectorSO : PressCommandBuildDirectorSO
    {
        protected override ICommand BuildInverse(IWeightedFloorPad floorPad, int candidateWeight)
        {
            Action execute;

            if (floorPad is OneWayFloorPadBehaviour oneWayFloorPadBehaviour)
            {
                execute = oneWayFloorPadBehaviour.PressInverse;
            }
            else
            {
                execute = floorPad.Release;
            }

            return CommandBuilder.WithCandidateWeight(candidateWeight).WithCanExecute(floorPad.CanRelease)
                                 .WithExecute(execute).WithAdjustWeight(floorPad.RemoveWeight)
                                 .WithCommandDescription("Floor pad press inverse").Build();
        }
    }

}
