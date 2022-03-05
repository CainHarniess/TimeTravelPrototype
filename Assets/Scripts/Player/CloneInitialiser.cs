using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class CloneInitialiser : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;
        private WeightedObjectTriggerHandler _triggerHandler;

        [SerializeField] private bool _IsActive;

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
    }
}
