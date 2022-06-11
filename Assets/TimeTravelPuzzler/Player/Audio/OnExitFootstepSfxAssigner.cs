using Osiris.TimeTravelPuzzler.Player.Audio;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class OnExitFootstepSfxAssigner : FootstepSfxAssignerBase
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Logger.Log($"{other.gameObject.name} exited trigger zone", GameObjectName);
            if (!other.TryGetComponent<PlayerFootstepSfxClipManager>(out var clipManager))
            {
                return;
            }
            clipManager.AssignClip(ClipData);
        }
    }
}
