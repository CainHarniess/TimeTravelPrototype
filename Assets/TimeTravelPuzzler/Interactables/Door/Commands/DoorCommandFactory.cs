using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors.Commands
{
    public class DoorCommandFactory : IFactory<IRewindableCommand, DoorInteractionType>
    {
        private readonly IDoor _door;

        public DoorCommandFactory(IDoor door)
        {
            _door = door;
        }

        public IRewindableCommand Create(DoorInteractionType parameter)
        {
            DelegateCommand inverse;
            if (parameter == DoorInteractionType.Open)
            {
                inverse = new DelegateCommand(_door.CanClose, _door.Close, "Door close");
                return new RewindableDelegateCommand(_door.CanOpen, _door.Open, "Door open", inverse);
            }

            inverse = new DelegateCommand(_door.CanOpen, _door.Open, "Door open");
            return new RewindableDelegateCommand(_door.CanClose, _door.Close, "Door close", inverse);
        }
    }
}
