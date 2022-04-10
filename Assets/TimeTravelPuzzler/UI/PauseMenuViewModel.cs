using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class PauseMenuViewModel : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private GameObject _MainCameraPrefab;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

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
