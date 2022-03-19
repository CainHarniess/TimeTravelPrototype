using Osiris.TimeTravelPuzzler.Core.Logging;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class CloneInitialiser : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;
        private PressableTriggerHandler _triggerHandler;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _deactivationDelay = 1;
        [SerializeField] private Transform _playerTransform;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [ReadOnly] [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;
        [SerializeField] private ReplayEventChannelSO _replayCompletedChannel;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _triggerHandler = GetComponent<PressableTriggerHandler>();

            if (_logger == null)
            {
                _logger = new NullConsoleLogger();
            }
        }

        private void Start()
        {
            SetStatus(false);
        }

        public void Activate(Vector3 position)
        {
            transform.position = position;
            SetStatus(true);
        }

        public void Deactivate()
        {
            SetStatus(false);
        }

        private void DelayedDeactivation()
        {
            _logger.Log("Rewind completion received.", gameObject);
            StartCoroutine(DeactivateWithDelay());
        }

        private IEnumerator DeactivateWithDelay()
        {
            yield return new WaitForSeconds(_deactivationDelay);
            Deactivate();
        }

        private void SetStatus(bool isActive)
        {
            if (_IsActive == isActive)
            {
                return;
            }

            _sprite.enabled = isActive;
            _collider.enabled = isActive;
            _triggerHandler.enabled = isActive;

            _IsActive = isActive;
        }

        private void OnEnable()
        {
            _replayCompletedChannel.Event += DelayedDeactivation;
        }

        private void OnDisable()
        {
            _replayCompletedChannel.Event -= DelayedDeactivation;
        }
    }
}
