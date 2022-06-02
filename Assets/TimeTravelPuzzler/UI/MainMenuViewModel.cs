using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Events;
using Osiris.Utilities.References;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class MainMenuViewModel : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO _GameplayPersistant;
        [SerializeField] private SceneSO _MainMenuScene;
        [SerializeField] private FloatReference _TransitionDurationRef;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _SceneChangeChannel;
        [SerializeField] private ApplicationEventChannel _ApplicationExit;
        [SerializeField] private TransitionChannelSO _TransitionChannel;
        [SerializeField] private EventChannelSO _MusicRequested;

        private float TransitionDuration => _TransitionDurationRef.Value;

        private void Start()
        {
            _MusicRequested.Raise();
        }

        public void Play()
        {
            _TransitionChannel.StartTransitionStep(isTransitionOut: true);

            StartCoroutine(ExecuteAfterDelay(LoadGameplayPersistantScene, TransitionDuration));
        }

        private void LoadGameplayPersistantScene()
        {
            var asyncOp = SceneManager.LoadSceneAsync(_GameplayPersistant.BuildIndex, LoadSceneMode.Additive);
            asyncOp.completed += OnGameplayPersistantLoaded;
        }

        private void OnGameplayPersistantLoaded(AsyncOperation asyncOp)
        {
            asyncOp.completed -= OnGameplayPersistantLoaded;
            SceneManager.UnloadSceneAsync(_MainMenuScene.BuildIndex);
        }

        public void ApplicationExit()
        {
            Logger.Log("Exit clicked.", GameObjectName);
            _ApplicationExit.Raise();
        }
    }
}
