using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.References;
using System;
using System.Collections;
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

        private float TransitionDuration => _TransitionDurationRef.Value;

        public void Play()
        {
            Logger.Log("Play clicked.", GameObjectName);
            _TransitionChannel.StartTransitionStep(isTransitionOut: true);

            StartCoroutine(ExecuteAfterTransition(() =>
            {
                var asyncOp = SceneManager.LoadSceneAsync(_GameplayPersistant.BuildIndex, LoadSceneMode.Additive);
                asyncOp.completed += OnGameplayPersistantLoaded;
            }));
        }

        private IEnumerator ExecuteAfterTransition(Action onCompleted)
        {
            yield return new WaitForSeconds(TransitionDuration);
            onCompleted();
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
