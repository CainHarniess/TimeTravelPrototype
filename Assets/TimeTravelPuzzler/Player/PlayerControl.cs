using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public abstract class PlayerControl : MonoBehaviour
    {
        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isControlActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [SerializeField] private PauseEventChannel _GameUnpaused;

        protected bool IsControlActive { get => _isControlActive; set => _isControlActive = value; }
        protected PauseEventChannel GamePaused { get => _GamePaused; }
        protected PauseEventChannel GameUnpaused { get => _GameUnpaused; }

        private void Start()
        {
            _isControlActive = true;
        }

        protected virtual void DeactivateControl()
        {
            _isControlActive = false;
        }

        protected virtual void ActivateControl()
        {
            _isControlActive = true;
        }

        protected virtual void OnEnable()
        {
            _GamePaused.Event += DeactivateControl;
            _GameUnpaused.Event += ActivateControl;
        }

        protected virtual void OnDisable()
        {
            _GamePaused.Event += DeactivateControl;
            _GameUnpaused.Event += ActivateControl;
        }
    }
}
