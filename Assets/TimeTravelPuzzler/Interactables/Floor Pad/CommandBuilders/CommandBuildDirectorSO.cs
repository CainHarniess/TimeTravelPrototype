using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Commands;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class CommandBuildDirectorSO : ScriptableObject
    {
        [SerializeField] private CommandBuilderSO _CommandBuilder;
        protected CommandBuilderSO CommandBuilder => _CommandBuilder;

        public IRewindableCommand Construct(IWeightedFloorPad floorPad, int candidateWeight)
        {
            ICommand inverse = BuildInverse(floorPad, candidateWeight);
            _CommandBuilder.Reset();
            return BuildCommand(floorPad, candidateWeight, inverse);
        }

        protected abstract ICommand BuildInverse(IWeightedFloorPad floorPad, int candidateWeight);

        protected abstract IRewindableCommand BuildCommand(IWeightedFloorPad floorPad, int candidateWeight, ICommand inverse);
    }

}
