using Osiris.EditorCustomisation;
using Osiris.Utilities.Audio;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Audio
{
    public class PlayerFootstepSfxClipManager : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _FootstepAudioSource;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _FootstepAudioSource,
                                                   nameof(_FootstepAudioSource));
        }

        public void AssignClip(AudioClipData clipData)
        {
            if (_FootstepAudioSource.clip == clipData.Clip)
            {
                Logger.Log("Current and target audio clips are the same.", GameObjectName,
                           LogLevel.Warning);
            }
            _FootstepAudioSource.clip = clipData.Clip;
        }
    }
}
