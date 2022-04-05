using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public abstract class FloorPadInteractorBase : MonoBehaviour
    {
        private string _gameObjectName;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private IntReference _WeightReference;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        protected OUL.ILogger Logger { get => _Logger; }
        protected int Weight => _WeightReference.Value;
        protected string GameObjectName
        {
            get
            {
                if (_gameObjectName == null)
                {
                    _gameObjectName = gameObject.name;
                }
                return _gameObjectName;
            }
        }

        private void Awake()
        {
            _Logger.Configure();
        }
    }
}