using Osiris.EditorCustomisation;
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

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private FloatReference _ColliderCastDistance;
        [SerializeField] private FloatReference _MovementDurationRef;
        [SerializeField] private PlayerMovementBuildDirector _MovementBuildDirector;
        [SerializeField] private Animator _Animator;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private IPlayerMovement _PlayerMovement;

        private float MovementDuration => _MovementDurationRef.Value;
        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            _cachedTransform = transform;

            this.IsInjectionPresent(_Logger, nameof(_Logger));
            this.IsInjectionPresent(_MovementDurationRef, nameof(_MovementDurationRef));
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator), gameObject);

            var collider = GetComponent<BoxCollider2D>();
            _PlayerMovement = _MovementBuildDirector.Construct(collider, _ColliderCastDistance.Value, transform, Logger,
                                                               GameObjectName);
        }

        public bool CanMove(Vector2 movementDirection)
        {
            return _PlayerMovement.CanMove(movementDirection);
        }

        public void Move(Vector2 movementDirection)
        {
            //_playerMovement.Move(movementDirection);
            StartCoroutine(ContinuousMove(movementDirection));
        }

        public IEnumerator ContinuousMove(Vector2 movementDirection)
        {
            _Animator.SetBool(AnimationParameters.IsMoving, true);
            _Animator.SetTrigger(AnimationParameters.IsMovingTrigger);
            float startTime = Time.time;
            float currentTime = startTime;

            Vector3 startPosition = _cachedTransform.position;
            Vector3 endPosition = startPosition + movementDirection.ToVector3();

            float currentProgress = 0;

            while (currentProgress < 1)
            {
                currentProgress = (currentTime - startTime) / MovementDuration;
                _cachedTransform.position = Vector3.Lerp(startPosition,
                                                         endPosition,
                                                         currentProgress);
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            _Animator.SetBool(AnimationParameters.IsMoving, false);
        }
    }
}
