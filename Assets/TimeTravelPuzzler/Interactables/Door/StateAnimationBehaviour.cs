using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactable;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{

    public class StateAnimationBehaviour : AnimationBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _OpenSprite;
        [SerializeField] private Sprite _ClosedSprite;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
