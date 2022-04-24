using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public abstract class AnimationBehaviour : MonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private Animator _Animator;

        protected Animator Animator => _Animator;
    }
}
