using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using OUL = Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadInteractorBehaviourBase : MonoBehaviour
    {
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _WeightReference;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        protected OUL.ILogger Logger { get => _Logger; }
        protected int Weight => _WeightReference.Value;

        private void Awake()
        {
            _Logger.Configure();
        }
    }
}