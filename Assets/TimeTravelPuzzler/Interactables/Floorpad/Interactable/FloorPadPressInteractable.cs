﻿using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadPressInteractable : FloorPadInteractable
    {
        public FloorPadPressInteractable(string gameObjectName, IFactory<IRewindableCommand, int> commandFactory,
                                         ILogger logger, IEventChannelSO interacted,
                                         IEventChannelSO<IRewindableCommand> recordableActionOccurred)
            : base(gameObjectName, commandFactory, logger, interacted, recordableActionOccurred)
        {

        }

        protected override string CanExecuteFailedMessage => "Press request rejected.";

        protected override string CanExecutePassedMessage => "Press request approved.";
    }
}