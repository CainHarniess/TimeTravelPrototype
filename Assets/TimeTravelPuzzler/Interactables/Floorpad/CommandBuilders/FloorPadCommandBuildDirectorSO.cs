using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class FloorPadCommandBuildDirectorSO : ScriptableObject
    {
        [SerializeField] private FloorPadCommandBuilderSO _CommandBuilder;
        protected FloorPadCommandBuilderSO CommandBuilder => _CommandBuilder;

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
