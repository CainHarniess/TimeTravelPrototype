﻿using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadReleaseInteractableBehaviour : FloorPadInteractableBehaviour, IReleaseInteractable
    {
        protected override IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad)
        {
            return new FloorPadReleaseCommandFactory(FloorPad);
        }

        protected override IInteractable<int> GetInteractable()
        {
            return new FloorPadReleaseInteractable(GameObjectName, GetFactory(FloorPad), Logger, Interacted,
                                                   RecordableActionOccurred);
        }
    }
}