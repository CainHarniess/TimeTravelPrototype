using Osiris.EditorCustomisation;
using System;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    public class AnimationCompletionEventDispatcherBehaviour : MonoBehaviour
    {
        public event Action AnimationCompleted;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private Animator _Animator;

        private void Awake()
        {
            if (_Animator == null)
            {
                _Animator.GetComponent<Animator>();
            }
        }
#pragma warning disable IDE0051  // Method is called by an animation event.
        private void OnCompleted()
        {
            AnimationCompleted?.Invoke();
        }
#pragma warning restore  IDE0051         
    }
}
