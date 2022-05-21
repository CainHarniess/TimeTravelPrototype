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

        private readonly string _methodName = "OnCompleted";
        private void OnCompleted()
        {
            AnimationCompleted?.Invoke();
        }
    }
}
