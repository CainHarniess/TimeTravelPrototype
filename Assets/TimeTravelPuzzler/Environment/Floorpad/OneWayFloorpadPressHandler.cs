using Osiris.TimeTravelPuzzler.Core.Interactions;

namespace Osiris.TimeTravelPuzzler.Environment.Floorpad
{

    public class OneWayFloorpadPressHandler : FloorpadPressHandler
    {
        public override bool CanPress(int additionalWeight)
        {
            if (IsPressed)
            {
                return false;
            }

            CurrentPressWeight += additionalWeight;

            if (CurrentPressWeight < MinPressWeight)
            {
                return false;
            }
            return true;
        }

        public override void Press()
        {
            SetPressStatus(true);
            PressedEvent.Invoke();
        }

        public override bool CanRelease(int weightToRemove)
        {
            return false;
        }

        public override void Release()
        {
            SetPressStatus(false);
            ReleasedEvent.Invoke();
        }
    }
}