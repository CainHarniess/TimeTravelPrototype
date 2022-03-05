using Osiris.TimeTravelPuzzler.Core.References;
using Osiris.TimeTravelPuzzler.Environment;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class WeightedObjectTriggerHandler : MonoBehaviour
    {
        [SerializeField] private IntReference _Weight;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<IPressable>() is IPressable pressable)
            {
                if (!pressable.CanPress(_Weight.Value))
                {
                    return;
                }
                pressable.Press();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<IPressable>() is IPressable pressable)
            {
                if (!pressable.CanRelease(_Weight.Value))
                {
                    return;
                }
                pressable.Release();
            }
        }
    }
}
