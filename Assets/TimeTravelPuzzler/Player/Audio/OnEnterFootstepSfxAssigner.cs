using Osiris.TimeTravelPuzzler.Player.Audio;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{

    public class OnEnterFootstepSfxAssigner : FootstepSfxAssignerBase
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            Logger.Log($"{other.gameObject.name} entered trigger zone", GameObjectName);
            if (!other.TryGetComponent<PlayerFootstepSfxClipManager>(out var clipManager))
            {
                return;
            }
            clipManager.AssignClip(ClipData);
        }
    }
}
