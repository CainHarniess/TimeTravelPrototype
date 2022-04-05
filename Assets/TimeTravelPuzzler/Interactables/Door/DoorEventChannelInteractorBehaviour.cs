using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class DoorEventChannelInteractorBehaviour : EventChannelInteractorBehaviour
    {
        [Header(InspectorHeaders.Usability)]
        [Tooltip(ToolTips.DoorInteractionType)]
        [SerializeField] private DoorInteractionType _InteractionType;

        [Header(InspectorHeaders.ControlVariables)]
        [Tooltip(ToolTips.Interactables)]
        [SerializeField] private DoorInteractableBehaviour[] _interactables;

        public override IInteractable[] Interactables => _interactables;



        private enum DoorInteractionType
        {
            Open,
            Close,
        }

        private struct ToolTips
        {
            public const string DoorInteractionType = "Label to aid channel and interactable assignment."
                                                        + "\n"
                                                        + "\nThis field does not control any functionality.";

            public const string Interactables = "The set of DoorInteractableBehaviour objects to be operated by invocations by "
                                                + "the above channel."
                                                + "\n"
                                                + "\nThe interaction depends on the current status of the interactable behavior. "
                                                + "The Interaction Type label has no influence.";
        }
    }
}
