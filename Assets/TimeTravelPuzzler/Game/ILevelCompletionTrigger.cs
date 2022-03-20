namespace Osiris.TimeTravelPuzzler.Game
{
    public interface ILevelCompletionTrigger
    {
        void TriggerLevelCompletion();
        void UndoLevelCompletion();
    }
}
