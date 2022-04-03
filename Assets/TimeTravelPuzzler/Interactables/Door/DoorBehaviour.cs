using Osiris.EditorCustomisation;
using Osiris.Testing;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class DoorBehaviour : MonoBehaviour, IDoor
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeReference] private IDoor _Door;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _sprite = GetComponent<SpriteRenderer>();

            var colliderProxy = new BehaviourProxy(_collider);
            var rendererProxy = new RendererProxy(_sprite);

            _Door = new Door(gameObject.name, _Logger, rendererProxy, colliderProxy);
        }

        public bool IsOpen => _Door.IsOpen;

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
        }

        private void SetComponentStatus(bool isActive)
        {
            _sprite.enabled = isActive;
            _collider.enabled = isActive;
        }
    }
}
