using Osiris.EditorCustomisation;
using Osiris.Utilities.Audio;
using UnityEngine;

namespace Osiris.GameManagement
{
    public class PauseManagerBehaviour : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioClipData _PauseSfx;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PauseEventChannel _PlayerPauseEventChannel;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [SerializeField] private PauseEventChannel _GameUnpaused;
        [SerializeField] private AudioClipDataEventChannel _SfxRequested;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _IsPaused;

        protected override void Awake()
        {
            base.Awake();
            _IsPaused = false;
        }

        private void OnPausePressed()
        {
            Logger.Log("Pause press received.", GameObjectName);

            if (_IsPaused)
            {
                Unpause();
                return;
            }
            Pause();
        }

        private void Pause()
        {
            Logger.Log("Game paused.", GameObjectName);
            _GamePaused.Raise();
            _SfxRequested.Raise(_PauseSfx);
            Time.timeScale = 0;
            _IsPaused = true;
        }

        private void Unpause()
        {
            Logger.Log("Game unpaused.", GameObjectName);
            _GameUnpaused.Raise();
            Time.timeScale = 1;
            _IsPaused = false;
        }

        private void OnEnable()
        {
            _PlayerPauseEventChannel.Event += OnPausePressed;
        }

        private void OnDisable()
        {
            _PlayerPauseEventChannel.Event -= OnPausePressed;
        }
    }
}
