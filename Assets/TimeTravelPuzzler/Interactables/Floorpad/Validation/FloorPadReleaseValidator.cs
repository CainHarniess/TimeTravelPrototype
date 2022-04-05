using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Validation;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation
{
    public class FloorPadReleaseValidator : FloorPadValidator, IValidator<int>
    {
        public FloorPadReleaseValidator(IFloorPad floorPad, OUL.ILogger logger, string gameObjectName)
            : base(floorPad, logger, gameObjectName)
        {

        }

        public override bool IsValid(int weightChange)
        {
            if (weightChange < 0)
            {
                Logger.Log("Weight to remove may not be negative.", GameObjectName, OUL.LogLevel.Error);
                return false;
            }

            if (FloorPad.CurrentPressWeight - weightChange < 0)
            {
                Logger.Log("Remaining weight on floor pad after removal may not be negative.",
                           GameObjectName, OUL.LogLevel.Error);
                return false;
            }
            return true;
        }
    }
}