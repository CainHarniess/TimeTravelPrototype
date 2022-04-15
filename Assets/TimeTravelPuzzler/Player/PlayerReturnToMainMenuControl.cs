using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class PlayerReturnToMainMenuControl : MonoBehaviour
    {
        [SerializeField] SceneSequencerBehaviour _sequencer;

        private void ReturnToMainMenu()
        {
            _sequencer.ReturnToMainMenu();
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}
