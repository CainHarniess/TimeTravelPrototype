using Osiris.TimeTravelPuzzler.Core.Events;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Environment
{

    public class WeightedPressurePad : MonoBehaviour, IPressable
    {
        [SerializeField] private int _minPressWeight;
        [ReadOnly]
        [SerializeField] private int _pressWeight;
        [ReadOnly]
        [SerializeField] private bool _isPressed;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private GameEventSO Pressed;
        [SerializeField] private GameEventSO Released;

        public bool IsPressed => _isPressed;

        public bool CanPress(int additionalWeight)
        {
            _pressWeight += additionalWeight;
            if (_pressWeight >= _minPressWeight)
            {
                return true;
            }
            return false;
        }

        public void Press()
        {
            Switch(true);
            Pressed.Invoke();
        }

        public bool CanRelease(int weightToRemove)
        {
            _pressWeight -= weightToRemove;
            if (_pressWeight < _minPressWeight)
            {
                return true;
            }
            return false;
        }

        public void Release()
        {
            Switch(false);
            Released.Invoke();
        }

        private void Switch(bool targetStatus)
        {
            if (_isPressed == targetStatus)
            {
                return;
            }
            _isPressed = targetStatus;
        }
    }
}