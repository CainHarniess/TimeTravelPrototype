using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadInteractorBase : OsirisMonoBehaviour, ILoggableBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;
        [SerializeField] private IntReference _WeightReference;

        protected int Weight => _WeightReference.Value;

        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            this.IsInjectionPresent(_Logger, nameof(_Logger));
            this.IsInjectionPresent(_WeightReference, nameof(_WeightReference));
        }
    }
}