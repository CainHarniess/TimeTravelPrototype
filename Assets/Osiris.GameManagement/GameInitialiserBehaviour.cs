using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.GameManagement
{
    public class GameInitialiserBehaviour : MonoBehaviour
    {
        [SerializeField] private SceneSO _PersistantApplication;
        [SerializeField] private SceneSO[] _ScenesToLoad;
        [SerializeField] private SceneSO[] _ScenesToUnload;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private int _scenesToLoadCount;
        [ReadOnly] [SerializeField] private int _scenesLoadedCount;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _StartUp;

        

        private void Start()
        {
            var asyncOp = SceneManager.LoadSceneAsync(_PersistantApplication.BuildIndex, LoadSceneMode.Additive);
            asyncOp.completed += OnPersistantSceneLoaded;
        }

        private void OnPersistantSceneLoaded(AsyncOperation asyncOp)
        {
            _StartUp.Raise(_ScenesToLoad, _ScenesToUnload);
            asyncOp.completed -= OnPersistantSceneLoaded;
        }
    }
}
