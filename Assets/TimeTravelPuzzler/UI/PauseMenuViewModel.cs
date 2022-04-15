using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.TimeTravelPuzzler.GameManagement;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class PauseMenuViewModel : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
#if UNITY_EDITOR
        [Tooltip(ToolTips.MainCameraPrefab)]
        [SerializeField] private GameObject _MainCameraPrefab;
#endif
        [Tooltip(ToolTips.PauseMenuUI)]
        [SerializeField] private GameObject _PauseMenuUI;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [Tooltip(ToolTips.PlayerPaused)]
        [SerializeField] private PauseEventChannel _PlayerPaused;
        [Tooltip(ToolTips.ReturnToMainMenu)]
        [SerializeField] private GameNavigationChannel _ReturnToMainMenu;

        [Header(InspectorHeaders.ListensTo)]
        [Tooltip(ToolTips.GamePaused)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [Tooltip(ToolTips.GameUnpaused)]
        [SerializeField] private PauseEventChannel _GameUnpaused;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
#if UNITY_EDITOR
            CameraColdStartUp();
#endif
        }

        public void Resume()
        {
            _PlayerPaused.Raise();
        }

        public void Options()
        {
            _Logger.Log("Options clicked.", _gameObjectName);
        }

        public void MainMenu()
        {
            _Logger.Log("Main Menu clicked.", _gameObjectName);
            _PlayerPaused.Raise();
            _ReturnToMainMenu.Raise();
        }

        private void OnPaused()
        {
            if (_PauseMenuUI.activeInHierarchy)
            {
                _Logger.Log("Pause UI is already inactive in the scene hierarchy.", _gameObjectName, LogLevel.Warning);
            }
            _PauseMenuUI.SetActive(true);
        }

        private void OnUnpaused()
        {
            if (!_PauseMenuUI.activeInHierarchy)
            {
                _Logger.Log("Pause UI is already active in the scene hierarchy.", _gameObjectName, LogLevel.Warning);
            }
            _PauseMenuUI.SetActive(false);
        }

        private void OnEnable()
        {
            _GamePaused.Event += OnPaused;
            _GameUnpaused.Event += OnUnpaused;
        }

        private void OnDisable()
        {
            _GamePaused.Event -= OnPaused;
            _GameUnpaused.Event -= OnUnpaused;
        }


#if UNITY_EDITOR
        /// <summary>
        /// Instantiates a main camera in the scene at run time if one is not already present in the scene.
        /// </summary>
        /// <remarks>
        /// Methos is only defined when running in the Unity editor.
        /// </remarks>
        private void CameraColdStartUp()
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
            {
                return;
            }
            GameObject.Instantiate(_MainCameraPrefab);
        }
#endif

        private struct ToolTips
        {
            public const string MainCameraPrefab = "Edit mode only. Creates a new main camera instance if one is not found at start-up.";
            
            public const string PauseMenuUI = "This will be activate/de-activated when a pause notification is received on the channels below.";
            
            public const string PlayerPaused = "Consumed by the Resume button to unpause the game via the PauseManager class.\n"
                                               + "\n"
                                               + "Should align with the instance consumed by the PlayerPauseControl class";
           
            public const string GamePaused = "Activates the pause menu UI when notification is received from the channel.\n"
                                             + "\n"
                                             + "This should match up with the instance used by the PauseManager class.";
            
            public const string GameUnpaused = "Used to de-activate the pause menu UI when notification is received from the channel.\n"
                                               + "\n"
                                               + "This should match up with the instance used by the PauseManager class.";

            public const string ReturnToMainMenu = "Should match up with the instance consumed by the SceneSequencerBehaviour class";
        }
    }
}
