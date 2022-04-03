using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Logging;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class CloneInitialiser : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _deactivationDelay = 1;
        [SerializeField] private Transform _playerTransform;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _logger;
        [ReadOnly] [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private ReplayEventChannelSO _replayCompletedChannel;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();

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
            if (_IsActive)
            {
                return;
            }
            SetStatus(true);
            transform.position = position;
            _replayCompletedChannel.Event += DelayedDeactivation;
        }

        public void Deactivate()
        {
            if (!_IsActive)
            {
                return;
            }
            SetStatus(false);
            _replayCompletedChannel.Event -= DelayedDeactivation;
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
            _sprite.enabled = isActive;
            _collider.enabled = isActive;
            _IsActive = isActive;
        }
    }
}
