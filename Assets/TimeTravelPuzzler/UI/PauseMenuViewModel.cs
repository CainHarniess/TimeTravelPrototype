using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class PauseMenuViewModel : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private GameObject _MainCameraPrefab;
        [SerializeField] private GameObject _PauseMenuUI;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private PauseEventChannel _GamePaused;
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
            _Logger.Log("Resume clicked.", _gameObjectName);
        }

        public void Options()
        {
            _Logger.Log("Options clicked.", _gameObjectName);
        }

        public void MainMenu()
        {
            _Logger.Log("Main Menu clicked.", _gameObjectName);
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
    }
}
