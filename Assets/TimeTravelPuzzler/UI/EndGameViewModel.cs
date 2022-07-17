using Osiris.EditorCustomisation;
using Osiris.SceneManagement.Core.Events;
using Osiris.TimeTravelPuzzler.GameManagement;
using Osiris.Utilities.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Osiris.TimeTravelPuzzler.UI
{
    public class EndGameViewModel : MonoBehaviour
    {
        [Header(InspectorHeaders.Injections)]
        [SerializeField] private EventSystem _EventSystem;
        [SerializeField] private GameObject _MenuButton;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private GameNavigationChannel _ReturnToMainMenuFromEndGame;
        [SerializeField] private EventChannelSO _EndGameReached;

        private void Start()
        {
            _EventSystem.SetSelectedGameObject(_MenuButton);
            _EndGameReached.Raise();
        }

        public void MainMenu()
        {
            _ReturnToMainMenuFromEndGame.Raise();
        }
    }
}
