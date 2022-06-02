using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Player.Audio;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{

    public class PlayerMovementBehaviour : OsirisMonoBehaviour, IPlayerMovement, ILoggableBehaviour
    {
        private Transform _cachedTransform;
        private WaitForEndOfFrame _cachedWaitInstance;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private FloatReference _ColliderCastDistance;
        [SerializeField] private FloatReference _MovementDurationRef;
        [SerializeField] private PlayerMovementBuildDirector _MovementBuildDirector;
        [ReadOnly] [SerializeField] private Animator _Animator;
        [ReadOnly] [SerializeField] private SpriteRenderer _Sprite;
        [SerializeField] private SpriteFlipperUtility _SpriteFlipper;
        [SerializeField] private FootstepSfxPlayer _footstepSfx;

        [Header(InspectorHeaders.ControlVariables)]
        [Tooltip(ToolTips.EqualsThreshold)]
        [ReadOnly] [SerializeField] private float _EqualsThreshold = 0.02f;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeReference] private IPlayerMovement _PlayerMovement;
        [ReadOnly] [SerializeField] private bool _IsMoving;

        private float MovementDuration => _MovementDurationRef.Value;
        public ILogger Logger => _Logger;

        private WaitForEndOfFrame CachedWaitInstance
        {
            get
            {
                if (_cachedWaitInstance == null)
                {
                    _cachedWaitInstance = new WaitForEndOfFrame();
                }
                return _cachedWaitInstance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _cachedTransform = transform;

            this.IsInjectionPresent(_Logger, nameof(_Logger));
            this.IsInjectionPresent(_MovementDurationRef, nameof(_MovementDurationRef));
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator));
            this.AddComponentInjectionIfNotPresent(ref _Sprite, nameof(_Sprite));
            this.AddComponentInjectionIfNotPresent(ref _footstepSfx, nameof(_footstepSfx));

            var collider = GetComponent<BoxCollider2D>();
            _PlayerMovement = _MovementBuildDirector.Construct(collider, _ColliderCastDistance.Value, transform,
                                                               Logger, GameObjectName, _EqualsThreshold);
            _IsMoving = false;
        }

        public bool CanMove(Vector2 movementDirection)
        {
            if (_IsMoving)
            {
                return false;
            }
            return _PlayerMovement.CanMove(movementDirection);
        }

        public void Move(Vector2 movementDirection)
        {
            StartCoroutine(ContinuousMove(movementDirection));
        }

        public IEnumerator ContinuousMove(Vector2 movementDirection)
        {
            _IsMoving = true;
            _Animator.SetBool(AnimationParameters.IsMoving, true);
            _SpriteFlipper.FlipSpriteIfRequired(_Sprite, movementDirection);

            float startTime = Time.time;
            float currentTime = startTime;

            Vector3 startPosition = _cachedTransform.position;
            Vector3 endPosition = startPosition + movementDirection.ToVector3();

            float currentProgress = 0;

            _footstepSfx.PlaySfx();

            while (currentProgress < 1)
            {
                currentProgress = (currentTime - startTime) / MovementDuration;
                _cachedTransform.position = Vector3.Lerp(startPosition,
                                                         endPosition,
                                                         currentProgress);
                currentTime += Time.deltaTime;
                yield return CachedWaitInstance;
            }

            _footstepSfx.PlaySfx();

            _cachedTransform.position = endPosition;

            _Animator.SetBool(AnimationParameters.IsMoving, false);
            _IsMoving = false;
        }




        private struct ToolTips
        {
            public const string EqualsThreshold = "The distance between two Vector2 instances below which they "
                                                  + "are considered equal.\n"
                                                  + "\n"
                                                  + "This overrides the Unity default of 1e-5 because the so that "
                                                  + "the normal surface collision calculation works correctly.";
        }
    }
}
