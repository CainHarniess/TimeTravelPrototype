using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.Utilities.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private GameEventSO _gameEvent;

        [SerializeField] private UnityEvent _eventHandler;

        private void OnEnable()
        {
            _gameEvent.Register(this);
        }

        public void OnEventRaised()
        {
            _eventHandler.Invoke();
        }

        private void OnDisable()
        {
            _gameEvent.Unregister(this);
        }
    }
}
