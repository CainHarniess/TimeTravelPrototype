using Osiris.EditorCustomisation;
using Osiris.Utilities.Audio;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class ButtonSfxPlayer : MonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioClipData _SelectionSfx;
        [SerializeField] private AudioClipData _SubmissionSfx;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private AudioClipDataEventChannel _SfxRequested;

        private void Awake()
        {
            Validate();
        }

        private void Validate()
        {
            if (_SelectionSfx == null)
            {
                Debug.LogError($"Sound effect has not been assigned to the on-selection event of {gameObject.name}.");
            }

            if (_SubmissionSfx == null)
            {
                Debug.LogError($"Sound effect has not been assigned to the on-submission event of {gameObject.name}.");
            }
        }

        public void OnSelected()
        {
            _SfxRequested.Raise(_SelectionSfx);
        }

        public void OnSubmitted()
        {
            _SfxRequested.Raise(_SubmissionSfx);
        }
    }
}
