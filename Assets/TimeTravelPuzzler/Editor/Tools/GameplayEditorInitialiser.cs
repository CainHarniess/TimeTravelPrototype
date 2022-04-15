using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.EditorTools
{
#if UNITY_EDITOR
    public class GameplayEditorInitialiser : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private SceneLoadUtilitySO _sceneLoadUtility;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _ApplicationPersistantScene;
        [SerializeField] private SceneSO _GameplayPersistantScene;
        [SerializeField] private SceneSO _PauseMenuScene;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
        }

        public void Start()
        {
            LoadPersistantSceneIfNotLoaded();
            LoadGameplayPersistantSceneIfNotLoaded();
            LoadPauseMenuSceneIfNotLoaded();
        }

        private void LoadPersistantSceneIfNotLoaded()
        {
            _sceneLoadUtility.LoadSceneIfNotAlreadyLoaded(_ApplicationPersistantScene, _gameObjectName, _Logger);
        }

        private void LoadGameplayPersistantSceneIfNotLoaded()
        {
            _sceneLoadUtility.LoadSceneIfNotAlreadyLoaded(_GameplayPersistantScene, _gameObjectName, _Logger);
        }

        private void LoadPauseMenuSceneIfNotLoaded()
        {
            _sceneLoadUtility.LoadSceneIfNotAlreadyLoaded(_PauseMenuScene, _gameObjectName, _Logger);
        }
    }
#endif
}
