using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Environment
{
    public class SwitchTriggerHandler : MonoBehaviour
    {
        private IPressurePad _prerssurePad;

        private IPressurePad PressurePad
        {
            get
            {
                if (_prerssurePad == null)
                {
                    _prerssurePad = GetComponent<IPressurePad>();
                }
                return _prerssurePad;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PressurePad.Press();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            PressurePad.Release();
        }
    }
}
