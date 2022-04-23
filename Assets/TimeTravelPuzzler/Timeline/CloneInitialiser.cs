using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.Utilities;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class CloneInitialiser : OsirisMonoBehaviour, ILoggableBehaviour
    {
        private SpriteRenderer _sprite;
        private BoxCollider2D _collider;
        private Animator _animator;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private Transform _PlayerTransform;
        [SerializeField] private SpriteRenderer _PlayerSprite;
        [SerializeField] private IntReference _DeactivationDelayReference;
        [SerializeField] private CoroutineTimer _PostRewindDectivationTimer;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool _IsActive;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _RewindStarted;
        [SerializeField] private ReplayEventChannelSO _ReplayCompleted;

        public ILogger Logger => _Logger;
        private int DeactivationDelay => _DeactivationDelayReference.Value;

        protected override void Awake()
        {
            base.Awake();
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();

            this.IsInjectionPresent(_Logger, nameof(_Logger));
            GetPlayerReferences();
        }

        private void Start()
        {
            Deactivate();
            _PostRewindDectivationTimer = new CoroutineTimer(DeactivationDelay, Deactivate);
        }

        [ContextMenu("Activate")]
        public void Activate()
        {
            if (_IsActive)
            {
                return;
            }
            SetStatus(true);
            _sprite.flipX = _PlayerSprite.flipX;
            transform.position = _PlayerTransform.position;
            _ReplayCompleted.Event += DelayedDeactivation;
        }

        [ContextMenu("Deactivate")]
        public void Deactivate()
        {
            if (!_IsActive)
            {
                return;
            }
            SetStatus(false);
            _ReplayCompleted.Event -= DelayedDeactivation;
        }

        private void DelayedDeactivation()
        {
            StartCoroutine(_PostRewindDectivationTimer.StartTimer());
        }

        private void SetStatus(bool isActive)
        {
            _sprite.enabled = isActive;
            _collider.enabled = isActive;
            _animator.enabled = isActive;
            _IsActive = isActive;
        }
        
        private void GetPlayerReferences()
        {
            this.AddComponentInjectionByTagIfNotPresent(ref _PlayerTransform,
                                                                    nameof(_PlayerTransform),
                                                                    Tags.Player);
            this.AddComponentInjectionByTagIfNotPresent(ref _PlayerSprite,
                                                        nameof(_PlayerSprite),
                                                        Tags.Player);
        }

        private void OnEnable()
        {
            _RewindStarted.Event += Activate;
            _ReplayCompleted.Event += DelayedDeactivation;
        }

        private void OnDisable()
        {
            _RewindStarted.Event -= Activate;
            _ReplayCompleted.Event -= DelayedDeactivation;
        }
    }
}
