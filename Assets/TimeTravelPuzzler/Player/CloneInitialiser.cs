using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class CloneInitialiser : MonoBehaviour, ILoggableBehaviour
    {
        private string _gameObjectName;
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private Transform _PlayerTransform;
        [SerializeField] private IntReference _DeactivationDelay;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private ReplayEventChannelSO _ReplayCompletedChannel;

        public string GameObjectName => _gameObjectName;
        public ILogger Logger => _Logger;

        private void Awake()
        {
            _gameObjectName = gameObject.name;
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();

            this.IsInjectionPresent(_Logger, nameof(_Logger).ToEditorName());
            this.AddComponentInjectionByTagIfNotPresent(ref _PlayerTransform,
                                                        nameof(_PlayerTransform).ToEditorName(),
                                                        Constants.PlayerTag);
        }

        private void Start()
        {
            Deactivate();
        }

        [ContextMenu("Activate")]
        public void Activate()
        {
            if (_IsActive)
            {
                return;
            }
            SetStatus(true);
            transform.position = _PlayerTransform.position;
            _ReplayCompletedChannel.Event += DelayedDeactivation;
        }

        [ContextMenu("Deactivate")]
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
