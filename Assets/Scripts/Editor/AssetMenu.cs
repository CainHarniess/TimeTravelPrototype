namespace Osiris.TimeTravelPuzzler.EditorCustomisation
{
    public struct AssetMenu
    {
        public const string MenuRoot = "ScriptableObjects/";

        public const string TimelineEventChannelFileName = "TimelineEventChannel";
        public const string TimelineEventChannelPath = AssetMenu.MenuRoot + "TimelineEventChannel";

        public const string RewindEventChannelFileName = "RewindEventChannel";
        public const string RewindEventChannelPath = AssetMenu.MenuRoot + "RewindEventChannel";

        public const string ReplayEventChannelFileName = "New Replay Event Channel";
        public const string ReplayEventChannelPath = AssetMenu.MenuRoot + "ReplayEventChannel";

        public const string LevelCompletionEventChannelFileName = "LevelCompletionEventChannel";
        public const string LevelCompletionEventChannelPath = AssetMenu.MenuRoot + "LevelCompletionEventChannel";

        public const string MoverFileName = "New Mover";
        public const string MoverPath = AssetMenu.MenuRoot + "Mover";

        public const string GameEventFileName = "New Game Event";
        public const string GameEventPath = AssetMenu.MenuRoot + "GameEvent";
    }
}
