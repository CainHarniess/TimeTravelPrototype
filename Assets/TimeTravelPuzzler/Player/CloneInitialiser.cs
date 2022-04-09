using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class CloneInitialiser : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _DeactivationDelay;
        [SerializeField] private Transform _PlayerTransform;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [ReadOnly] [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private ReplayEventChannelSO _ReplayCompletedChannel;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();

            _Logger.Configure();
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
            _ReplayCompletedChannel.Event += DelayedDeactivation;
        }

        public void Deactivate()
        {
            if (!_IsActive)
            {
                return;
            }
            SetStatus(false);
            _ReplayCompletedChannel.Event -= DelayedDeactivation;
        }

        private void DelayedDeactivation()
        {
            _Logger.Log("Rewind completion received.", gameObject);
            StartCoroutine(DeactivateWithDelay());
        }

        private IEnumerator DeactivateWithDelay()
        {
            yield return new WaitForSeconds(_DeactivationDelay.Value);
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
