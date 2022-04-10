using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler
{
#if UNITY_EDITOR
    public class MenuEditorInitialiser : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _PersistantScene;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
        }

        private void Start()
        {
            LoadPersistantSceneIfNotLoaded();
        }

        private void LoadPersistantSceneIfNotLoaded()
        {
            if (SceneManager.GetSceneByBuildIndex(_PersistantScene.BuildIndex).isLoaded)
            {
                _Logger.Log("Persistant scene is already loaded.", _gameObjectName);
                return;
            }
            _Logger.Log("Persistant load being loaded.", _gameObjectName);
            SceneManager.LoadSceneAsync(_PersistantScene.BuildIndex, LoadSceneMode.Additive);
        }
    }
#endif
}
