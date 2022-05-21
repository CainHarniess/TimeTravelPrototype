using UnityEngine;

namespace Osiris.Utilities.Editor.Animation
{
    [CreateAssetMenu(fileName = AssetMenu.AddOnStartAnimationEventStrategyFileName,
                     menuName = AssetMenu.AddOnStartAnimationEventPath)]
    public class OnStartAnimationEventAdditionUtility : AnimationEventAdditionUtility
    {
        public override void AddAnimationEvent(AnimationClip clipToUpdate, string functionName)
        {
            AnimationEvent newEvent = new AnimationEvent()
            {
                time = 0,
                functionName = functionName,
            };

            clipToUpdate.AddEventToClip(newEvent);
        }
    }
}





