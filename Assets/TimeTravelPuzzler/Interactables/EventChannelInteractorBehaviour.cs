using Osiris.EditorCustomisation;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class EventChannelInteractorBehaviour : MonoBehaviour, IInteractor
    {
        [Header(InspectorHeaders.ListensTo)]
        [Tooltip(ToolTips.Channel)]
        [SerializeField] private EventChannelSO _Channel;

        public abstract IInteractable[] Interactables { get; }

        protected virtual void OnEnable()
        {
            _Channel.Event += HandleInteractions;
        }

        protected virtual void OnDisable()
        {
            _Channel.Event -= HandleInteractions;
        }

        private void HandleInteractions()
        {
            foreach(IInteractable interactable in Interactables)
            {
                interactable.Interact();
            }
        }



        private struct ToolTips
        {
            public const string Channel = "The channel to which this interactor behavior will listen."
                                          + "\n"
                                          + "\nInvocations on this channel will trigger an interaction with the below.";
        }
    }

}
