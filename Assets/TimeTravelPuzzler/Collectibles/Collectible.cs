using Osiris.EditorCustomisation;
using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Collectibles
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Collectible : LoggableMonoBehaviour, ICollectible
    {
        private BoxCollider2D _collider;
        private SpriteRenderer[] _spriteRenderers;
        private AudioSource _audioSource;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private EventChannelSO _Collected;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Collect()
        {
            Logger.Log("Collected.", GameObjectName);
            _audioSource.Play();
            Deactivate();
            _Collected.Raise();
        }

        private void Deactivate()
        {
            _collider.enabled = false;
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}
