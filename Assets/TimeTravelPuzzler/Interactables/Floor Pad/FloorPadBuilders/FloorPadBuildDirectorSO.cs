using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using ILogger = Osiris.Utilities.Logging.ILogger;
using UnityEngine;
using Osiris.Utilities.ScriptableObjects;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadBuildDirectorSO : DescriptionSO
    {
        [SerializeField] private FloorPadBuilderSO _FloorPadBuilder;
        protected FloorPadBuilderSO FloorPadBuilder => _FloorPadBuilder;
        public abstract IWeightedFloorPad Construct(IWeightedFloorPad floorPadBehaviour, ILogger logger,
                                                    string gameObjectName, IFloorPadSpriteHandler spriteHandler,
                                                    IEventChannelSO pressedChannel, IEventChannelSO releasedChannel);
    }
}