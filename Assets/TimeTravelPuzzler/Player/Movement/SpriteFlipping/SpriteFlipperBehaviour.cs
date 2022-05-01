using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class SpriteFlipperBehaviour : OsirisMonoBehaviour, ILoggableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private SpriteRenderer _Sprite;

        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            this.IsInjectionPresent(_Logger, nameof(_Logger));
        }

        public void PointBackwards()
        {
            _Sprite.flipX = true;
        }

        public void PointForwards()
        {
            _Sprite.flipX = false;
        }
    }
}
