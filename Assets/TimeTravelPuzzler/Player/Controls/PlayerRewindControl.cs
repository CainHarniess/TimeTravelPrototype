using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewindControl : PlayerControl, ILoggableBehaviour
    {
        private string _gameObjectName;
        private PlayerInput _playerInput;
        private InputAction _rewindAction;
        private IEnumerator _rewindTimer;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private FloatReference _MaximumRewindTimeRef;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private CloneInitialiser _CloneInitialiser;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        public string GameObjectName => _gameObjectName;
        public ILogger Logger => _Logger;

        void Awake()
        {
            _gameObjectName = gameObject.name;
            
            this.IsInjectionPresent(_Logger, nameof(_Logger).ToEditorName());

            string initialiserName = nameof(_CloneInitialiser).ToEditorName();
            this.AddComponentInjectionByTagIfNotPresent(ref _CloneInitialiser, initialiserName,
                                                        Constants.PlayerCloneTag);

            _playerInput = GetComponent<PlayerInput>();
            _rewindAction = _playerInput.actions[ControlActions.RewindTime];
        }

        private void OnRewindStarted(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            _PlayerRewindRequested.Raise();

            // TODO:    The TimelineManager should be responsible for activating the clone once
            //          the rewind request has been approved.
            _CloneInitialiser.Activate();

            _rewindTimer = RewindTimer();
            StartCoroutine(_rewindTimer);
        }

        private IEnumerator RewindTimer()
        {
            yield return new WaitForSeconds(_MaximumRewindTimeRef.Value);
            _PlayerRewindCancelled.Raise();
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            if (_rewindTimer != null)
            {
                StopCoroutine(_rewindTimer);
            }
            _PlayerRewindCancelled.Raise();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _rewindAction.performed += OnRewindStarted;
            _rewindAction.canceled += OnRewindCancelled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _rewindAction.performed -= OnRewindStarted;
            _rewindAction.canceled -= OnRewindCancelled;
        }
    }
}
