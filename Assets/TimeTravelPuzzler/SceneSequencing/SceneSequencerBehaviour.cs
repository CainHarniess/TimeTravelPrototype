using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler
{
    public class SceneSequencerBehaviour : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _PersistantScene;
        [SerializeField] private SceneSO[] _LevelArray;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private int _currentLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _CurrentLevel;
        [ReadOnly] [SerializeField] private int _nextLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _NextLevel;
        [ReadOnly] [SerializeField] private int _previousLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _PreviousLevel;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannelSO _SceneSequencerChannel;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _SceneChangeChannel;

        private void Awake()
        {
            CacheGameObjectName();
        }

        private void Start()
        {
            GetCurrentSceneReferences();
            GetPreviousLevelReferences();
            GetNextLevelReference();
        }

        private void IterateScenes()
        {
            _SceneChangeChannel.Raise(new SceneSO[] { _NextLevel }, new SceneSO[] { _CurrentLevel });
            _Logger.Log($"Loading: {_NextLevel.Name}. Unloading: {_CurrentLevel.Name}", _gameObjectName);
            IterateLevelReferences();
        }

        private void GetCurrentSceneReferences()
        {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            _currentLevelSequenceIndex = Array.FindIndex(_LevelArray, sd => sd.BuildIndex == currentSceneBuildIndex);
            _CurrentLevel = _LevelArray[_currentLevelSequenceIndex];
        }
        private void GetPreviousLevelReferences()
        {
            _previousLevelSequenceIndex = (_currentLevelSequenceIndex == 0)
                            ? _LevelArray.Length - 1
                            : _currentLevelSequenceIndex - 1;
            _PreviousLevel = _LevelArray[_previousLevelSequenceIndex];
        }

        private void GetNextLevelReference()
        {
            _nextLevelSequenceIndex = (_currentLevelSequenceIndex == _LevelArray.Length - 1)
                            ? 0
                            : _currentLevelSequenceIndex + 1;
            _NextLevel = _LevelArray[_nextLevelSequenceIndex];
        }

        private void IterateLevelReferences()
        {
            _PreviousLevel = _CurrentLevel;
            _previousLevelSequenceIndex = _currentLevelSequenceIndex;

            _CurrentLevel = _NextLevel;
            _currentLevelSequenceIndex = _nextLevelSequenceIndex;

            _nextLevelSequenceIndex = (_nextLevelSequenceIndex + 1) % _LevelArray.Length;
            _NextLevel = _LevelArray[_nextLevelSequenceIndex];
            _Logger.Log($"New next level: {_NextLevel.Name}.", _gameObjectName);

        }

        private void CacheGameObjectName()
        {
            if (_gameObjectName == null)
            {
                _gameObjectName = gameObject.name;
            }
        }

        private void OnEnable()
        {
            _SceneSequencerChannel.Event += IterateScenes;
        }

        private void OnDisable()
        {
            _SceneSequencerChannel.Event -= IterateScenes;
        }
    }
}
