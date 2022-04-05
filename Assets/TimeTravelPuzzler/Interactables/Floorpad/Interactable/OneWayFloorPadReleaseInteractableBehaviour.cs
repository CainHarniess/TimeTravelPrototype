namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPadReleaseInteractableBehaviour : FloorPadReleaseInteractableBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            CommandFactory = new OneWayFloorPadReleaseCommandFactory(FloorPad);
        }
    }
}