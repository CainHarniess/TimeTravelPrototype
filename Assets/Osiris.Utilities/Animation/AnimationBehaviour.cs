using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.Utilities.Animation
{
    public abstract class AnimationBehaviour : OsirisMonoBehaviour, ILoggableBehaviour, IInjectableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private Animator _Animator;

        protected Animator Animator => _Animator;
        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator));
        }
    }
}
