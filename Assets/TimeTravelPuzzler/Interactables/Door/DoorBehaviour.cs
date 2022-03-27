using Osiris.EditorCustomisation;
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
            _Door = new Door(gameObject.name, _Logger);
        }

        public bool IsOpen => _Door.IsOpen;

        public bool CanOpen()
        {
            return _Door.CanOpen();
        }

        public void Open()
        {
            _Door.Open();
            SetComponentStatus(false);
        }

        public bool CanClose()
        {
            return _Door.CanClose();
        }

        public void Close()
        {
            _Door.Close();
            SetComponentStatus(true);
        }

        private void SetComponentStatus(bool isActive)
        {
            _sprite.enabled = isActive;
            _collider.enabled = isActive;
        }
    }
}
