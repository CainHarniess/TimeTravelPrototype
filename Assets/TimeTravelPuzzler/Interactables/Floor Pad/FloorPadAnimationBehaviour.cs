using Osiris.Utilities.DependencyInjection;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadAnimationBehaviour : LoggableMonoBehaviour, IInjectableBehaviour
    {
        [SerializeField] private Animator _Animator;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator));
        }

        public void OnPress()
        {
            _Animator.SetTrigger("Pressed");
        }

        public void OnRelease()
        {
            _Animator.SetTrigger("Released");
        }
    }
}
