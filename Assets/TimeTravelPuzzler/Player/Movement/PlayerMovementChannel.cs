using Osiris.Utilities.Events;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    [CreateAssetMenu(fileName = AssetMenu.MovementChannelFileName,
        menuName = AssetMenu.MovementChannelPath)]
    public class PlayerMovementChannel : EventChannelSO<Vector2>
    { 
        
    }
}
