using Osiris.EditorCustomisation;
using Osiris.Utilities.Audio;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class DoorSfxPlayer : InteractableSfxPlayerBase
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioClipData _OpenClipData;
        [SerializeField] private AudioClipData _CloseClipData;

        public void OnOpen()
        {
            AudioSource.PlayOneShot(_OpenClipData.Clip);
        }

        public void OnClose()
        {
            AudioSource.PlayOneShot(_CloseClipData.Clip);
        }
    }
}
