using Osiris.TimeTravelPuzzler.Core.Interactions;
using Osiris.Utilities.References;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    [Obsolete("Class has been refactored. Use FloorPadInteractorBehaviour instead.", true)]
    public class PressableTriggerHandler : MonoBehaviour
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
