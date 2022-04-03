using Osiris.EditorCustomisation;
using Osiris.Utilities.Editor;
using Osiris.Utilities.Logging;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.Utilities.Events
{
    [CreateAssetMenu(fileName = AssetMenu.EventChannelFileName, menuName = AssetMenu.EventChannelPath)]
    public class EventChannelSO : DescriptionSO, IEventChannelSO
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        protected UnityConsoleLogger Logger => _Logger;

        [SerializeField] public event UnityAction Event;

        public void Raise()
        {
            if (Event != null)
            {
                _Logger.Log($"Event request received on channel.", name);
                Event.Invoke();
            }
            else
            {
                _Logger.Log("Event request received on channel, but no listeners are configured.", name);
            }
        }
    }

    [CreateAssetMenu(fileName = AssetMenu.EventChannel1FileName, menuName = AssetMenu.EventChannel1Path)]
    public class EventChannelSO<T> : ScriptableObject, IEventChannelSO<T>
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [SerializeField] public event UnityAction<T> Event;

        protected UnityConsoleLogger Logger => _Logger;

        public void Raise(T parameter)
        {
            if (Event != null)
            {
                _Logger.Log($"Event request received on channel with parameter value {parameter}.", name);
                Event.Invoke(parameter);
            }
            else
            {
                _Logger.Log("Event request received on channel with parameter value {parameter}, "
                            + "but no listeners are configured.", name);
            }
        }
    }
}
