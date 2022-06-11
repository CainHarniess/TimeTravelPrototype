using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Audio;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorpadSfxPlayer : InteractableSfxPlayerBase, IFloorPadBehaviourHandler
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioClipData _PressClipData;
        [SerializeField] private AudioClipData _ReleaseClipData;

        public void OnPress()
        {
            AudioSource.PlayOneShot(_PressClipData.Clip);
        }

        public void OnRelease()
        {
            AudioSource.PlayOneShot(_ReleaseClipData.Clip);
        }
    }
}
