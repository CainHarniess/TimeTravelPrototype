using Osiris.EditorCustomisation;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.LevelManagement
{
    public class LevelCompletionController : MonoBehaviour
    {
        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private EventChannelSO _TriggerEvent;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private LevelCompletionEventChannel _SceneSequencerChannel;

        protected void TriggerSceneChange()
        {
            _SceneSequencerChannel.Raise();
        }

        private void OnEnable()
        {
            _TriggerEvent.Event += TriggerSceneChange;
        }

        private void OnDisable()
        {
            _TriggerEvent.Event -= TriggerSceneChange;
        }
    }
}
