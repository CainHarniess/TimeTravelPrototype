using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler
{

#if UNITY_EDITOR
#endif


#if UNITY_EDITOR
    public class GameplayEditorInitialiser : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _ApplicationPersistantScene;
        [SerializeField] private SceneSO _GameplayPersistantScene;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
            LoadPersistantSceneIfNotLoaded();
            LoadGameplayPersistantSceneIfNotLoaded();
        }

        private void LoadPersistantSceneIfNotLoaded()
        {
            if (SceneManager.GetSceneByBuildIndex(_ApplicationPersistantScene.BuildIndex).isLoaded)
            {
                _Logger.Log("Persistant scene is already loaded.", _gameObjectName);
                return;
            }
            _Logger.Log("Persistant load being loaded.", _gameObjectName);
            SceneManager.LoadSceneAsync(_ApplicationPersistantScene.BuildIndex, LoadSceneMode.Additive);
        }

        private void LoadGameplayPersistantSceneIfNotLoaded()
        {
            if (SceneManager.GetSceneByBuildIndex(_GameplayPersistantScene.BuildIndex).isLoaded)
            {
                _Logger.Log("Gameplay persistant scene is already loaded.", _gameObjectName);
                return;
            }
            _Logger.Log("Gameplay persistant load being loaded.", _gameObjectName);
            SceneManager.LoadSceneAsync(_GameplayPersistantScene.BuildIndex, LoadSceneMode.Additive);
        }
    }
#endif
}
