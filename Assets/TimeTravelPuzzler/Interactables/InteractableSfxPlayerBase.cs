using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class InteractableSfxPlayerBase : MonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _AudioSource;

        protected AudioSource AudioSource => _AudioSource;

        private void Awake()
        {
            if (_AudioSource == null)
            {
                _AudioSource = GetComponent<AudioSource>();
            }
        }
    }
}
