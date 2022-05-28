using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Audio
{
    public class PlayerFootstepSfxClipManager : LoggableMonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _FootstepAudioSource;
        [SerializeField] private AudioClip _NormalFootstep;
        [SerializeField] private AudioClip _GrateFootstep;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _FootstepAudioSource,
                                                   nameof(_FootstepAudioSource));
            this.IsInjectionPresent(_NormalFootstep, nameof(_NormalFootstep));
            this.IsInjectionPresent(_GrateFootstep, nameof(_GrateFootstep));

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Logger.Log("OnTriggerEnter2D.", GameObjectName);

            TryChangeAudioClip(other, SetGrateFootstep);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Logger.Log("OnTriggerExit2D.", GameObjectName);
            TryChangeAudioClip(other, SetNormalFootstep);
        }

        private void TryChangeAudioClip(Collider2D other, Action setClipAction)
        {
            Logger.Log("Querying other collider.", GameObjectName);

            if (!(other.GetComponent<GrateTiles>() is GrateTiles))
            {
                Logger.Log("Component GrateTiles not found on candidate.",
                           GameObjectName);
                return;
            }

            setClipAction();
        }

        private void SetNormalFootstep()
        {
            _FootstepAudioSource.clip = _NormalFootstep;
        }

        private void SetGrateFootstep()
        {
            _FootstepAudioSource.clip = _GrateFootstep;
        }
    }
}
