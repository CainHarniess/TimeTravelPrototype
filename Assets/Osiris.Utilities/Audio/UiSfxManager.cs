using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.Utilities.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UiSfxManager : MonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _AudioSource;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private AudioClipDataEventChannel _SfxRequested;

        private void Awake()
        {
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

            _AudioSource.Play();
        }

        private void OnDisable()
        {
            _SfxRequested.Event -= PlaySfx;
        }
    }
}
