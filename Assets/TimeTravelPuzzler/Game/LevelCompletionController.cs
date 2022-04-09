using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core;
using Osiris.SceneManagement.Core.Events;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class LevelCompletionController : MonoBehaviour
    {
        [Header(InspectorHeaders.ControlVariables)]

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private LevelCompletionEventChannelSO _LevelCompleted;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private LevelCompletionEventChannelSO _SceneSequencerChannel;

        private void TriggerSceneChange()
        {
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
