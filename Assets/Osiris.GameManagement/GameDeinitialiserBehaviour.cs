using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using System;
using System.Collections;
using UnityEngine;

namespace Osiris.GameManagement
{
    public class GameDeinitialiserBehaviour : MonoBehaviour
    {
        private string _gameObjectName;


        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private SceneSO[] _ScenesToUnload;
        [SerializeField] private FloatReference _TransitionDuration;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private ApplicationEventChannel _ApplicationExit;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private SceneChangeEventSO _ShutDown;

        private float TransitionDuration => _TransitionDuration.Value;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
        }

        private void OnApplicationExit()
        {
            _Logger.Log("Application exit request received.", _gameObjectName);
            _ShutDown.Raise(Array.Empty<SceneSO>(), _ScenesToUnload);
            StartCoroutine(WaitForTransitionThenQuit());
        }

        private IEnumerator WaitForTransitionThenQuit()
        {
            _Logger.Log("Waiting for transition.", _gameObjectName);
            yield return new WaitForSeconds(TransitionDuration);
            _Logger.Log("Exiting application.", _gameObjectName);
            Application.Quit();
        }

        private void OnEnable()
        {
            _ApplicationExit.Event += OnApplicationExit;
        }

        private void OnDisable()
        {
            _ApplicationExit.Event -= OnApplicationExit;
        }
    }
}
