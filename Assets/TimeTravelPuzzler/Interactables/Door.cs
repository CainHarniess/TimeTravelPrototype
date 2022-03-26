using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class Door : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private SpriteRenderer Sprite
        {
            get
            {
                if (_sprite == null)
                {
                    _sprite = GetComponent<SpriteRenderer>();
                }
                return _sprite;
            }
        }

        private BoxCollider2D _collider;
        private BoxCollider2D Collider
        {
            get
            {
                if (_collider == null)
                {
                    _collider = GetComponent<BoxCollider2D>();
                }
                return _collider;
            }
        }

        [ReadOnly] [SerializeField] private bool _IsOpen;

        public void Open()
        {
            SetStatus(false);
            _IsOpen = true;
        }

        public void Close()
        {
            SetStatus(true);
            _IsOpen = false;
        }

        private void SetStatus(bool isActive)
        {
            Sprite.enabled = isActive;
            Collider.enabled = isActive;
        }
    }
}