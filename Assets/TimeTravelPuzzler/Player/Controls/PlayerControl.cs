using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.Utilities.References;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler
{
    [RequireComponent(typeof(PlayerInput))]
    public abstract class PlayerControl : LoggableMonoBehaviour
    {
        private PlayerInput _playerInput;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private FloatReference _SceneTransitionDuration;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _isControlActive = false;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [SerializeField] private PauseEventChannel _GameUnpaused;

        protected PlayerInput PlayerInput => _playerInput;
        protected bool IsControlActive { get => _isControlActive; set => _isControlActive = value; }
        protected PauseEventChannel GamePaused { get => _GamePaused; }
        protected PauseEventChannel GameUnpaused { get => _GameUnpaused; }

        protected override void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            // Blocks player input until the scene fade in has completed.
            StartCoroutine(ExecuteAfterDelay(ActivateControl, 0.75f * _SceneTransitionDuration.Value));
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
