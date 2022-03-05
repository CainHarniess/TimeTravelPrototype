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
        private WeightedObjectTriggerHandler _triggerHandler;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _deactivationDelay = 1;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _rewindCompleteEventChannel;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _triggerHandler = GetComponent<WeightedObjectTriggerHandler>();
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
            Debug.Log("Rewind completion received.");
            StartCoroutine(DeactivateWithDelay(_deactivationDelay));
        }

        private IEnumerator DeactivateWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
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
            _rewindCompleteEventChannel.RewindCompleted += DelayedDeactivation;
        }

        private void OnDisable()
        {
            _rewindCompleteEventChannel.RewindCompleted -= DelayedDeactivation;
        }
    }
}
