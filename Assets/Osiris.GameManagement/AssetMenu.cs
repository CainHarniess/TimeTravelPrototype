namespace Osiris.TimeTravelPuzzler
{
    internal struct AssetMenu
    {
        public const string MenuRoot = "ScriptableObjects/GameManagement/";

        public const string PauseEventChannelFileName = "New Pause Event Channel";
        public const string PauseEventChannelPath = MenuRoot + "PauseEventChannel";

        public const string GameNavigationEventChannelFileName = "New Game Navigation Event Channel";
        public const string GameNavigationEventChannelPath = MenuRoot + "GameNavigationEventChannel";

        public const string ApplicationExitChannelFileName = "New Application Event Channel";
        public const string ApplicationExitChannelPath = MenuRoot + "ApplicationEventChannel";
    }
}
