using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler
{
    public class EditorInitialiser : MonoBehaviour
    {
#if UNITY_EDITOR
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _PersistantScene;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        private void Awake()
        {
            CacheGameObjectName();
            if (SceneManager.GetSceneByBuildIndex(_PersistantScene.BuildIndex).isLoaded)
            {
                _Logger.Log("Persistant scene is already loaded.", _gameObjectName);
                return;
            }
            _Logger.Log("Persistant load being loaded.", _gameObjectName);
            SceneManager.LoadSceneAsync(_PersistantScene.BuildIndex, LoadSceneMode.Additive);
        }

        private void CacheGameObjectName()
        {
            if (_gameObjectName == null)
            {
                _gameObjectName = gameObject.name;
            }
        }
#endif
    }
}
