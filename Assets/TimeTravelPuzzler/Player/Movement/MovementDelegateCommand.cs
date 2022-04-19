using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Commands;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class MovementDelegateCommand : RewindableDelegateCommand<Vector2>
    {
        public MovementDelegateCommand(Func<Vector2, bool> canExecute, Action<Vector2> execute,
                                       Vector2 movementDirection)
            : base(canExecute, execute, movementDirection, null)
        {

        }

        public MovementDelegateCommand(Func<Vector2, bool> canExecute, Action<Vector2> execute,
                                       Vector2 movementDirection, string description)
            : base(canExecute, execute, movementDirection, description)
        {

        }

        public MovementDelegateCommand(Func<Vector2, bool> canExecute, Action<Vector2> execute,
                                       Vector2 movementDirection, string description, ICommand inverse)
            : base(canExecute, execute, movementDirection, description, inverse)
        {

        }
    }
}
