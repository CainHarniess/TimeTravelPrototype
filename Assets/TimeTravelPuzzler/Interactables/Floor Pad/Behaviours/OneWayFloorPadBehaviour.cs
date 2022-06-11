namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class OneWayFloorPadBehaviour : FloorPadBehaviour
    {
        public override void Release()
        {
            FloorPad.Release();
        }

        public void PressInverse()
        {
            if (!(FloorPad is OneWayWeightedFloorPad oneWayFloorPad))
            {
                return;
            }
            
            oneWayFloorPad.PressInverse();
            HandleRelease();
        }
    }
}