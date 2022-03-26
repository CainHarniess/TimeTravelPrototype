using Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPad : FloorPad
    {
        public OneWayFloorPad(IFloorPad floorPadBehaviour, ILogger logger, string gameObjectName)
            : base(floorPadBehaviour, logger, gameObjectName)
        {

        }

        public override bool CanRelease(int weightRemoved)
        {
            CurrentPressWeight -= weightRemoved;
            return false;
        }

        public override void Release()
        {
            Logger.Log("One-way floor pads may not be released.", GameObjectName, LogLevel.Error);
            base.Release();
        }
    }
}