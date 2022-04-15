using Osiris.TimeTravelPuzzler.Interactables.FloorPads;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Commands;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    [CreateAssetMenu(fileName = AssetMenu.OneWayPressCommandDirectorFileName, menuName = AssetMenu.OneWayPressCommandDirectorPath)]
    public class OneWayFloorPadPressCommandBuildDirectorSO : PressFloorPadCommandBuildDirectorSO
    {
        protected override ICommand BuildInverse(IWeightedFloorPad floorPad, int candidateWeight)
        {
            Action execute;

            if (floorPad is WeightedFloorPadBehaviour floorPadBehaviour
                && floorPadBehaviour.FloorPad is OneWayWeightedFloorPad oneWayFloorPad)
            {
                execute = oneWayFloorPad.PressInverse;
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
