using Osiris.EditorCustomisation;
using Osiris.Utilities.Animation;
using Osiris.Utilities.DependencyInjection;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class LightFlickerAnimationBehaviour : RegularTriggerAnimationBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private LightBehaviour _LightBehaviour;
        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _LightBehaviour, nameof(_LightBehaviour));
        }

        protected override void Start()
        {
            Animator.SetBool("IsOn", _LightBehaviour.IsOn);
            base.Start();
        }
    }
}
