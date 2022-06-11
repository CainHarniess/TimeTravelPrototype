using Osiris.Utilities.Audio;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Logging;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    public abstract class FootstepSfxAssignerBase : LoggableMonoBehaviour, IInjectableBehaviour
    {
        [SerializeField] private Collider2D _Collider;
        [SerializeField] private AudioClipData _ClipData;

        protected AudioClipData ClipData => _ClipData;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _Collider, nameof(_Collider));
        }

        private void Start()
        {
            if (!_Collider.isTrigger)
            {
                Logger.Log("Colider is not configured as a trigger.", GameObjectName,
                           LogLevel.Error);
            }
        }
    }
}
