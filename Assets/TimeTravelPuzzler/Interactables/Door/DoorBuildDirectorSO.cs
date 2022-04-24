using Osiris.TimeTravelPuzzler.Interactables.Doors;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;


namespace Osiris.TimeTravelPuzzler.Interactables
{
    [CreateAssetMenu(fileName = DoorAssetMenu.DoorBuildDirectorFileName, menuName = DoorAssetMenu.DoorBuildDirectorPath)]
    public class DoorBuildDirectorSO : DescriptionSO
    {
        [SerializeField] private DoorBuilderSO _doorBuilder;

        public IDoor Construct(string gameObjectName, ILogger logger, BoxCollider2D collider, bool isOpen)
        {
            return _doorBuilder.WithGameObjectName(gameObjectName).WithLogger(logger)
                               .WithBoxCollider2D(collider).WithStatus(isOpen).Build();
        }
    }
}
