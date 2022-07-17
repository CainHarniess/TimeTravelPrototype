using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.TimeTravelPuzzler.GameManagement;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class PauseMenuViewModel : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.ControlVariables)]
#if UNITY_EDITOR
        [Tooltip(ToolTips.MainCameraPrefab)]
        [SerializeField] private GameObject _MainCameraPrefab;
#endif
        [Tooltip(ToolTips.PauseMenuUI)]
        [SerializeField] private GameObject _PauseMenuUI;
        [SerializeField] private EventSystem _EventSystem;
        [SerializeField] private GameObject _ResumeButton;

        [Header(InspectorHeaders.BroadcastsOn)]
        [Tooltip(ToolTips.PlayerPaused)]
        [SerializeField] private PauseEventChannel _PlayerPaused;
        [Tooltip(ToolTips.ReturnToMainMenu)]
        [SerializeField] private GameNavigationChannel _ReturnToMainMenu;
        [SerializeField] private EventChannelSO _LevelRestarted;

        [Header(InspectorHeaders.ListensTo)]
        [Tooltip(ToolTips.GamePaused)]
        [SerializeField] private PauseEventChannel _GamePaused;
        [Tooltip(ToolTips.GameUnpaused)]
        [SerializeField] private PauseEventChannel _GameUnpaused;

        protected override void Awake()
        {
            base.Awake();

            if (_ResumeButton == null)
            {
                Logger.Log("Resume button game object not specified in the inspector ahead of run time.",
                           GameObjectName, LogLevel.Error);
            }
        }

        private void OnEnable()
        {
            _GamePaused.Event += OnPaused;
            _GameUnpaused.Event += OnUnpaused;
        }

        public void Resume()
        {
            _PlayerPaused.Raise();
        }

        public void Restart()
        {
            _PlayerPaused.Raise();
            _LevelRestarted.Raise();
        }

        public void MainMenu()
        {
            _PlayerPaused.Raise();
            _ReturnToMainMenu.Raise();
        }

        private void OnPaused()
        {
            if (_PauseMenuUI.activeInHierarchy)
            {
                Logger.Log("Pause UI is already active in the scene hierarchy.",
                           GameObjectName, LogLevel.Warning);
            }
            _PauseMenuUI.SetActive(true);
            SelectResumeButton();
        }

        private void OnUnpaused()
        {
            if (!_PauseMenuUI.activeInHierarchy)
            {
                Logger.Log("Pause UI is already inactive in the scene hierarchy.",
                           GameObjectName, LogLevel.Warning);
            }
            DeselectMenuButton();
            _PauseMenuUI.SetActive(false);

        }
        
        private void SelectResumeButton()
        {
            _EventSystem.SetSelectedGameObject(_ResumeButton);
        }

        private void DeselectMenuButton()
        {
            _EventSystem.SetSelectedGameObject(null);
        }


        private void OnDisable()
        {
            _GamePaused.Event -= OnPaused;
            _GameUnpaused.Event -= OnUnpaused;
        }


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
