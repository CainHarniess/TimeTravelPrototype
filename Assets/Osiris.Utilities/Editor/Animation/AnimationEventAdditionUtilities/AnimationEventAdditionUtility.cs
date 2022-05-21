using UnityEngine;

namespace Osiris.Utilities.Editor.Animation
{
    public abstract class AnimationEventAdditionUtility : ScriptableObject
    {
        public abstract void AddAnimationEvent(AnimationClip clipToUpdate, string functionName);
    }
}





