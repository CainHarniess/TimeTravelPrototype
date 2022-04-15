namespace Osiris.TimeTravelPuzzler.LevelManagement
{
    public interface ILevelCompletionTrigger
    {
        void TriggerLevelCompletion();
        void UndoLevelCompletion();
    }
}
