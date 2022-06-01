using TMPro;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class ButtonTextShader : MonoBehaviour
    {
        [SerializeField] private TMP_Text _Text;
        [SerializeField] private AudioSource _AudioSource;

        private void Update()
        {
            if (!_AudioSource.enabled)
                _AudioSource.enabled = true;
        }

        public void OnDeselected()
        {
            _Text.color = Color.grey;
        }

        public void OnSelected()
        {
            _Text.color = Color.white;
        }
    }
}
