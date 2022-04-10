using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.GameManagement
{
    public class PauseManagerBehaviour : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private bool _IsPaused;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PauseEventChannel _PlayerPauseEventChannel;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [SerializeField] private PauseEventChannel _GameUnpaused;

        void Awake()
        {
            _gameObjectName = gameObject.name;
            _IsPaused = false;
        }

        private void OnPausePressed()
        {
            _Logger.Log("Pause press received.", _gameObjectName);

            if (_IsPaused)
            {
                Unpause();
                return;
            }
            Pause();
        }

        private void Pause()
        {
            _Logger.Log("Game paused.", _gameObjectName);
            _GamePaused.Raise();
            Time.timeScale = 0;
            _IsPaused = true;
        }

        private void Unpause()
        {
            _Logger.Log("Game unpaused.", _gameObjectName);
            _GameUnpaused.Raise();
            Time.timeScale = 0;
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
