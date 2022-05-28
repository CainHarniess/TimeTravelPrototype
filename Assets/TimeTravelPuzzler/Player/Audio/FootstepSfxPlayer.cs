using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Audio
{
    public class FootstepSfxPlayer : LoggableMonoBehaviour, IInjectableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _FootstepAudioSource;
        [SerializeField] private FloatReference _FootstepInterval;


        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _FootstepAudioSource,
                                                   nameof(_FootstepAudioSource));
            this.IsInjectionPresent(_FootstepInterval, nameof(_FootstepInterval));
        }

        private void OnEnable()
        {
            // In order to show the enabled/disabled checkbox in the inspector
        }

        public void PlaySfx()
        {
            _FootstepAudioSource.PlayDelayed(_FootstepInterval.Value);
        }
    }
}
