using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler
{
    public class SceneSequencer : MonoBehaviour
    {
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO[] _LevelArray;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private SceneSO _CurrentLevel;
        [ReadOnly] [SerializeField] private SceneSO _NextLevel;
        [ReadOnly] [SerializeField] private SceneSO _PreviousLevel;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannelSO _SceneSequencerChannel;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _SceneChangeChannel;

        [ReadOnly] [SerializeField] private int _currentLevelSequenceIndex;
        [ReadOnly] [SerializeField] private int _nextLevelSequenceIndex;
        [ReadOnly] [SerializeField] private int _previousLevelSequenceIndex;
        public SceneSO CurrentLevel => _CurrentLevel;
        public SceneSO NextLevel => _NextLevel;
        public SceneSO PreviousLevel => _PreviousLevel;

        private void Start()
        {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            _Logger.Log($"Current scene build index: {currentSceneBuildIndex}", string.Empty);

            _currentLevelSequenceIndex = Array.FindIndex(_LevelArray, sd => sd.BuildIndex == currentSceneBuildIndex);
            _CurrentLevel = _LevelArray[_currentLevelSequenceIndex];

            _previousLevelSequenceIndex = ((_currentLevelSequenceIndex == 0)
                ? _LevelArray.Length 
                : _currentLevelSequenceIndex)
                - 1;
            _PreviousLevel = _LevelArray[_previousLevelSequenceIndex];

            _nextLevelSequenceIndex = (_currentLevelSequenceIndex == _LevelArray.Length - 1)
                ? 0
                : _currentLevelSequenceIndex + 1;
            _NextLevel = _LevelArray[_nextLevelSequenceIndex];
        }

        private void TriggerSceneChange()
        {
            _SceneChangeChannel.Raise(new SceneSO[] { _NextLevel }, new SceneSO[] { _CurrentLevel });
            _PreviousLevel = _CurrentLevel;
            _previousLevelSequenceIndex = _currentLevelSequenceIndex;
            _CurrentLevel = _NextLevel;
            _currentLevelSequenceIndex = _nextLevelSequenceIndex;

            _nextLevelSequenceIndex = (_nextLevelSequenceIndex + 1) % _LevelArray.Length;
            _NextLevel = _LevelArray[_nextLevelSequenceIndex];
        }

        private void OnEnable()
        {
            _SceneSequencerChannel.Event += TriggerSceneChange;
        }

        private void OnDisable()
        {
            _SceneSequencerChannel.Event -= TriggerSceneChange;
        }
    }
}
