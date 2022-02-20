using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [CreateAssetMenu(fileName = AssetMenu.RewindEventChannelFileName, menuName = AssetMenu.RewindEventChannelPath)]
    public class RewindEventChannelSO : DescriptionSO
    {
        public event UnityAction Event;

        public void Raise()
        {
            if (Event != null)
            {
                Event.Invoke();
            }
            else
            {
                Debug.LogWarning("A rewind event occured, but no listeners are configured.");
            }
        }
    }
}
