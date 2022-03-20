using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class LevelCompletionController : MonoBehaviour
    {
        [SerializeField] protected SceneSO[] _scenesToLoad;
        [SerializeField] protected SceneSO[] _scenesToUnload;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannelSO _levelCompletionChannel;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _sceneChangeChannel;

        private void Awake()
        {
            if (_scenesToLoad.Length == 0)
            {
                Debug.LogWarning("No scenes are scheduled for loading.");
            }

            if (_scenesToLoad.Length == 0)
            {
                Debug.LogWarning("No scenes are scheduled for unloading.");
            }
        }

        private void TriggerSceneChange()
        {
            _sceneChangeChannel.Raise(_scenesToLoad, _scenesToUnload);
        }

        private void OnEnable()
        {
            _levelCompletionChannel.LevelCompleted += TriggerSceneChange;
        }

        private void OnDisable()
        {
            _levelCompletionChannel.LevelCompleted -= TriggerSceneChange;
        }
    }
}
