using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.Utilities.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UiSfxManager : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _AudioSource;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private AudioClipDataEventChannel _SfxRequested;

        protected override void Awake()
        {
            base.Awake();

            if (_AudioSource == null)
            {
                _AudioSource = GetComponent<AudioSource>();
            }
        }

        private void OnEnable()
        {
            _SfxRequested.Event += PlaySfx;
        }

        public void PlaySfx(AudioClipData clipData)
        {
            if (_AudioSource.clip != clipData.Clip)
            {
                _AudioSource.clip = clipData.Clip;
            }

            _AudioSource.PlayDelayed(0);
        }

        private void OnDisable()
        {
            _SfxRequested.Event -= PlaySfx;
        }
    }
}
