using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactable.Doors.Animations;
using Osiris.Utilities.Animation;
using Osiris.Utilities.DependencyInjection;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors.Animations
{
    public class DoorStateAnimationBehaviour : AnimationBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _OpenSprite;
        [SerializeField] private Sprite _ClosedSprite;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _spriteRenderer, nameof(_spriteRenderer));
        }

        public void SetInitialState(bool isInitiallyOpen)
        {
            if (isInitiallyOpen)
            {
                Animator.SetBool(AnimationParameters.IsOpen, true);
                _spriteRenderer.sprite = _OpenSprite;
                return;
            }
            Animator.SetBool(AnimationParameters.IsOpen, false);
            _spriteRenderer.sprite = _ClosedSprite;
        }

        public void Open()
        {
            Animator.SetTrigger(AnimationParameters.DoorOpening);
            Animator.SetBool(AnimationParameters.IsOpen, true);
        }

        public void Close()
        {
            Animator.SetTrigger(AnimationParameters.DoorClosing);
            Animator.SetBool(AnimationParameters.IsOpen, false);
        }
    }
}
