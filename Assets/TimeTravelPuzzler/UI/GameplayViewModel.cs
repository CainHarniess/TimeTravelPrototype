using Osiris.EditorCustomisation;
using Osiris.Utilities.Events;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class GameplayViewModel : MonoBehaviour
    {
        private WaitForEndOfFrame _cachedEndOfFrameWait;
        private Coroutine _depletionCoroutine;
        private Coroutine _refillCoroutine;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private FloatReference _RewindAllowance;
        [SerializeField] private Slider _Slider;
        [SerializeField] private GameObject _GameplayUI;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _RefillSpeed = 1;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private EventChannelSO _RewindStarted;
        [SerializeField] private EventChannelSO _RewindCompleted;
        [SerializeField] private EventChannelSO _PlayerRewindCancelled;
        [SerializeField] private EventChannelSO _ReplayCompleted;
        [SerializeField] private EventChannelSO _EndGameReached;

        private WaitForEndOfFrame CachedEndOfFrameWait
        {
            get
            {
                if (_cachedEndOfFrameWait == null)
                {
                    _cachedEndOfFrameWait = new WaitForEndOfFrame();
                }
                return _cachedEndOfFrameWait;
            }
        }

        private void StartDepletion()
        {
            this.TryStopCoroutine(_refillCoroutine);
            this.TryStopCoroutine(_depletionCoroutine);

            _depletionCoroutine = StartCoroutine(Deplete());
        }

        private IEnumerator Deplete()
        {
            float rewindDuration = 0;

            while (rewindDuration <= _RewindAllowance.Value)
            {
                float currentValue = _RewindAllowance.Value - rewindDuration;
                _Slider.value = currentValue / _RewindAllowance.Value;

                rewindDuration += Time.deltaTime;

                yield return CachedEndOfFrameWait;
            }

            _Slider.value = 0;
        }

        private void StopDepletion()
        {
            this.TryStopCoroutine(_depletionCoroutine);
        }

        private void StartRefill()
        {
            _depletionCoroutine = StartCoroutine(RefillRewindJuice());
        }

        private IEnumerator RefillRewindJuice()
        {
            while (_Slider.value <= 1)
            {
                _Slider.value += _RefillSpeed * Time.deltaTime;
                yield return CachedEndOfFrameWait;
            }

            _Slider.value = 1;
            StopRefill();
        }

        private void StopRefill()
        {
            this.TryStopCoroutine(_refillCoroutine);
        }

        private void ActivateUI()
        {
            _GameplayUI.SetActive(true);
        }

        private void DeactivateUI()
        {
            _GameplayUI.SetActive(false);
        }

        private void OnEnable()
        {
            _RewindStarted.Event += StartDepletion;
            _RewindCompleted.Event += StopDepletion;
            _PlayerRewindCancelled.Event += StopDepletion;
            _ReplayCompleted.Event += StartRefill;
            _EndGameReached.Event += DeactivateUI;
        }
        
        private void OnDisable()
        {
            _RewindStarted.Event -= StartDepletion;
            _RewindCompleted.Event -= StopDepletion;
            _PlayerRewindCancelled.Event -= StopDepletion;
            _ReplayCompleted.Event -= StartRefill;
            _EndGameReached.Event -= DeactivateUI;
        }
    }
}
