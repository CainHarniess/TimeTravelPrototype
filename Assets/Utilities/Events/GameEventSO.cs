using Osiris.Utilities.Editor;
using Osiris.Utilities.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core.Events
{
    [CreateAssetMenu(fileName = AssetMenu.GameEventFileName, menuName = AssetMenu.GameEventPath)]
    public class GameEventSO : DescriptionSO
    {
        private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

        public void Register(GameEventListener listener)
        {
            _listeners.Add(listener);   
        }

        public void Unregister(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Invoke()
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventRaised();
            }
        }
    }
}
