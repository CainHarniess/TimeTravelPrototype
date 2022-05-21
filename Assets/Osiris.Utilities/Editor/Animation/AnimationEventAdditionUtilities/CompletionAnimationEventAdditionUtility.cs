using UnityEngine;

namespace Osiris.Utilities.Editor.Animation
{
    [CreateAssetMenu(fileName = AssetMenu.AddCompletionAnimationEventStrategyFileName,
                     menuName = AssetMenu.AddCompletionAnimationEventPath)]
    public class CompletionAnimationEventAdditionUtility : AnimationEventAdditionUtility
    {
        public override void AddAnimationEvent(AnimationClip clipToUpdate, string functionName)
        {
            AnimationEvent newEvent = new AnimationEvent()
            {
                time = clipToUpdate.length,
                functionName = functionName,
            };

            clipToUpdate.AddEventToClip(newEvent);
        }
    }
}





