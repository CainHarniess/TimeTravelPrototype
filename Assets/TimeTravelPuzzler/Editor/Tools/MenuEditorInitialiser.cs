using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler.EditorTools
{
#if UNITY_EDITOR
    public class MenuEditorInitialiser : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private SceneLoadUtilitySO _sceneLoadUtility;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _PersistantApplication;

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
            _sceneLoadUtility.LoadSceneIfNotAlreadyLoaded(_PersistantApplication, _gameObjectName, _Logger);
        }
    }
#endif
}
