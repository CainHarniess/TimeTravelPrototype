using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.SceneManagement.Extensions;
using Osiris.TimeTravelPuzzler.GameManagement;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler.LevelManagement
{
    public class SceneSequencerBehaviour : MonoBehaviour
    {
        private string _gameObjectName;

        [Header("Static Scenes")]
        [Tooltip(ToolTips.PersistantApplication)]
        [SerializeField] private SceneSO _PersistantApplication;

        [Tooltip(ToolTips.PersistantGameplay)]
        [SerializeField] private SceneSO _PersistantGameplay;

        [SerializeField] private SceneSO _PauseMenu;

        [Tooltip(ToolTips.MainMenu)]
        [SerializeField] private SceneSO _MainMenu;

        [Tooltip(ToolTips.LevelArray)]
        [SerializeField] private SceneSO[] _LevelArray;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private int _scenesToLoad = 2;
        [ReadOnly] [SerializeField] private int _scenesLoaded = 0;
        [ReadOnly] [SerializeField] private int _currentLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _CurrentLevel;
        [ReadOnly] [SerializeField] private int _nextLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _NextLevel;
        [ReadOnly] [SerializeField] private int _previousLevelSequenceIndex;
        [ReadOnly] [SerializeField] private SceneSO _PreviousLevel;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannel _SceneSequencerChannel;
        [SerializeField] private GameNavigationChannel _ReturnToMainMenu;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _SceneChangeChannel;
        [SerializeField] private TransitionChannelSO _TransitionChannel;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
        }

        private void Start()
        {
            GetCurrentSceneReferences();
            GetPreviousLevelReferences();
            GetNextLevelReference();

            var levelAsyncOp = SceneManager.LoadSceneAsync(_CurrentLevel.BuildIndex, LoadSceneMode.Additive);
            levelAsyncOp.completed += OnLevelLoaded;

            var pauseAsyncOp = SceneManager.LoadSceneAsync(_PauseMenu.BuildIndex, LoadSceneMode.Additive);
            pauseAsyncOp.completed += OnLevelLoaded;
        }

        private void OnLevelLoaded(AsyncOperation asyncOp)
        {
            asyncOp.completed -= OnLevelLoaded;
            _scenesLoaded++;

            if (_scenesLoaded < _scenesToLoad)
            {
                return;
            }

            OnAllScenesLoaded();
        }

        private void OnAllScenesLoaded()
        {
            _TransitionChannel.StartTransitionStep(isTransitionOut: false);
        }

        private void IterateScenes()
        {
            _SceneChangeChannel.Raise(new SceneSO[] { _NextLevel }, new SceneSO[] { _CurrentLevel });
            _Logger.Log($"Loading: {_NextLevel.Name}. Unloading: {_CurrentLevel.Name}", _gameObjectName);
            IterateLevelReferences();
        }

        private void GetCurrentSceneReferences()
        {
            _currentLevelSequenceIndex = 0;
            _CurrentLevel = _LevelArray[_currentLevelSequenceIndex];
        }
        private void GetPreviousLevelReferences()
        {
            _previousLevelSequenceIndex = _currentLevelSequenceIndex == 0
                            ? _LevelArray.Length - 1
                            : _currentLevelSequenceIndex - 1;
            _PreviousLevel = _LevelArray[_previousLevelSequenceIndex];
        }

        private void GetNextLevelReference()
        {
            _nextLevelSequenceIndex = _currentLevelSequenceIndex == _LevelArray.Length - 1
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

        public void ReturnToMainMenu()
        {
            var scenesToUnload = new SceneSO[]
            {
                _CurrentLevel,
                _PersistantGameplay,
                _PauseMenu
            };
            _Logger.Log(scenesToUnload.Stringify(), _gameObjectName);

            var scenesToLoad = new SceneSO[]
            {
                _MainMenu
            };
            _Logger.Log(scenesToLoad.Stringify(), _gameObjectName);

            _SceneChangeChannel.Raise(scenesToLoad, scenesToUnload);
        }

        private void OnEnable()
        {
            _SceneSequencerChannel.Event += IterateScenes;
            _ReturnToMainMenu.Event += ReturnToMainMenu;
        }

        private void OnDisable()
        {
            _SceneSequencerChannel.Event -= IterateScenes;
            _ReturnToMainMenu.Event -= ReturnToMainMenu;
        }






        private struct ToolTips
        {
            public const string PersistantApplication = "Doesn't seem to be used for anything in this class.\n"
                                                        + "\n"
                                                        + "Yet.";

            public const string PersistantGameplay = "Static reference to the persistant gameplay scene to aid loading"
                                                     + " and unloading.\n"
                                                     + "\n"
                                                     + "Used to unload the to unload the persistant gameplay scene"
                                                     + " when returning to the main menu";

            public const string MainMenu = "Static reference to the persistant gameplay scene to aid loading and "
                                           + "unloading";

            public const string LevelArray = "Captures the order in which levels will be iterated. Removes the need to"
                                             + " adjust the build settings to move levels around.";

        }
    }
}
