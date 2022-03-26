namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPadBehaviour : FloorPadBehaviour
    {
        protected override void Awake()
        {
            FloorPad = new OneWayFloorPad(this, Logger, gameObject.name);
        }
    }
}