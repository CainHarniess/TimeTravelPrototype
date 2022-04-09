using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class LevelCompletionController : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        //[SerializeField] protected SceneSO[] _scenesToLoad;
        //[SerializeField] protected SceneSO[] _scenesToUnload;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannelSO _LevelCompleted;

        [Header(InspectorHeaders.BroadcastsOn)]
        //[SerializeField] private SceneChangeEventSO _sceneChangeChannel;
        [SerializeField] private LevelCompletionEventChannelSO _SceneSequencerChannel;

        private string GameObjectName
        {
            get
            {
                if (_gameObjectName == null)
                {
                    _gameObjectName = gameObject.name;
                }
                return _gameObjectName;
            }
        }

        private void Awake()
        {
            //if (_scenesToLoad.Length == 0)
            //{
            //    _Logger.Log("No scenes are scheduled for loading.", GameObjectName, LogLevel.Warning);
            //}

            //if (_scenesToLoad.Length == 0)
            //{
            //    _Logger.Log("No scenes are scheduled for unloading.", GameObjectName, LogLevel.Warning);
            //}
        }

        private void TriggerSceneChange()
        {
            //_sceneChangeChannel.Raise(_scenesToLoad, _scenesToUnload);
            _SceneSequencerChannel.Raise();
        }

        private void OnEnable()
        {
            _LevelCompleted.Event += TriggerSceneChange;
        }

        private void OnDisable()
        {
            _LevelCompleted.Event -= TriggerSceneChange;
        }
    }
}
