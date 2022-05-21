using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Osiris.Utilities.Editor.Animation
{
    public static class AnimationClipExtensions
    {
        public static void AddEventToClip(this AnimationClip clipToUpdate, AnimationEvent newEvent)
        {
            List<AnimationEvent> events = clipToUpdate.events.ToList();

            if (events.Any(e => e.time == newEvent.time && e.functionName == newEvent.functionName))
            {
                Debug.LogWarning($"Animation clip {clipToUpdate.name} already contains an animation event "
                                 + $"at time {newEvent.time} with function name {newEvent.functionName}.");
                return;
            }

            events.Add(newEvent);
            AnimationUtility.SetAnimationEvents(clipToUpdate, events.ToArray());
        }
    }
}





