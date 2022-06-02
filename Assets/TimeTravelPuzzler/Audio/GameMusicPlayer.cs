using Osiris.EditorCustomisation;
using Osiris.Utilities.Audio;
using Osiris.Utilities.Events;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    [RequireComponent(typeof(AudioSource))]
    public class GameMusicPlayer : MonoBehaviour
    {
        private WaitForEndOfFrame _cachedWaitInstance;

        [SerializeField] private float _InitialVolume = 0;
        [SerializeField] private FloatReference _FadeDurationRef;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _AudioSource;
        [SerializeField] private AudioClipData _GameMusic;

        private void Awake()
        {
            if (_AudioSource == null)
            {
                _AudioSource = GetComponent<AudioSource>();
            }

            if (!_AudioSource.loop)
            {
                Debug.LogWarning("Music audio source should be set to loop in the inspector.");
                _AudioSource.loop = true;
            }

            if (_AudioSource.clip != _GameMusic.Clip)
            {
                _AudioSource.clip = _GameMusic.Clip;
            }

            _cachedWaitInstance = new WaitForEndOfFrame();
        }

        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float targetVolume = _AudioSource.volume;

            float initialVolume = _InitialVolume;
            _AudioSource.volume = initialVolume;
            float currentVolume = _AudioSource.volume;

            float startTime = Time.time;
            float currentTime = startTime;

            _AudioSource.Play();

            while (currentVolume < targetVolume)
            {
                currentVolume = (currentTime - startTime) / _FadeDurationRef.Value;
                _AudioSource.volume = Mathf.Lerp(initialVolume, targetVolume, currentVolume);
                currentTime += Time.deltaTime;
                yield return _cachedWaitInstance;
            }

            _AudioSource.volume = targetVolume;
        }
    }
}
