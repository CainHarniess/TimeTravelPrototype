using Osiris.TimeTravelPuzzler.Game;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class PlayerTriggerHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ILevelCompletionTrigger>() is ILevelCompletionTrigger levelCompletionTrigger)
            {
                levelCompletionTrigger.TriggerLevelCompletion();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<ILevelCompletionTrigger>() is ILevelCompletionTrigger levelCompletionTrigger)
            {
                levelCompletionTrigger.UndoLevelCompletion();
            }
        }
    }
}
