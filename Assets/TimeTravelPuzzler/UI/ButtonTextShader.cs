using TMPro;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class ButtonTextShader : MonoBehaviour
    {
        [SerializeField] private TMP_Text _Text;

        public void OnSelected()
        {
            _Text.color = Color.white;
        }

        public void OnDeselected()
        {
            _Text.color = Color.grey;
        }
    }
}
