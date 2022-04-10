using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class MainMenuViewModel : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private GameObject _MainCameraPrefab;
        [SerializeField] private SceneSO _MainMenuScene;
        [SerializeField] private SceneSO _FirstLevelScene;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _SceneChangeChannel;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
#if UNITY_EDITOR
            CameraColdStartUp();
#endif
        }


        public void Play()
        {
            _Logger.Log("Play clicked.", _gameObjectName);
            _SceneChangeChannel.Raise(new SceneSO[] { _FirstLevelScene }, new SceneSO[] { _MainMenuScene });
        }

        public void LoadOptionsMenu()
        {
            _Logger.Log("Options clicked.", _gameObjectName);
        }

        public void ApplicationExit()
        {
            _Logger.Log("Exit clicked.", _gameObjectName);

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
