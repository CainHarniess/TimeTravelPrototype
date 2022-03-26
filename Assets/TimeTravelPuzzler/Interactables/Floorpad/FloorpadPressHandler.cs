using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Events;
using Osiris.TimeTravelPuzzler.Core.Interactions;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class FloorpadPressHandler : MonoBehaviour, IPressable
    {
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _MinPressWeight;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private int _CurrentPressWeight;
        [ReadOnly] [SerializeField] private bool _IsPressed;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private GameEventSO _PressedEvent;
        [SerializeField] private GameEventSO _ReleasedEvent;

        protected int MinPressWeight { get => _MinPressWeight.Value; }
        protected int CurrentPressWeight { get => _CurrentPressWeight; set => _CurrentPressWeight = value; }

        protected GameEventSO PressedEvent => _PressedEvent;
        protected GameEventSO ReleasedEvent => _ReleasedEvent;


        public bool IsPressed { get => _IsPressed; protected set => _IsPressed = value; }
        public abstract bool CanPress(int additionalWeight);
        public abstract void Press();
        public abstract bool CanRelease(int weightRemoved);
        public abstract void Release();

        protected void SetPressStatus(bool isPressed)
        {
            if (IsPressed == isPressed)
            {
                Debug.LogWarning($"Attempting to set floorpad press status to {isPressed} when the press status is {IsPressed}");
                return;
            }
            IsPressed = isPressed;
        }
    }
}