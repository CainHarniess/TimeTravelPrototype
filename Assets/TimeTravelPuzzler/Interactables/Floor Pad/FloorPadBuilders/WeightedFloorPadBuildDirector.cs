using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities.Events;
using Osiris.Utilities.Validation;
using ILogger = Osiris.Utilities.Logging.ILogger;
using UnityEngine;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Validation;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    [CreateAssetMenu(fileName = AssetMenu.WeightedFloorPadBuildDirectorFileName, menuName = AssetMenu.WeightedFloorPadBuildDirectorPath)]
    public class WeightedFloorPadBuildDirector : FloorPadBuildDirectorSO
    {
        public override IWeightedFloorPad Construct(IWeightedFloorPad floorPadBehaviour, ILogger logger, string gameObjectName,
                                            IFloorPadSpriteHandler spriteHandler, IEventChannelSO pressedChannel,
                                            IEventChannelSO releasedChannel)
        {
            IValidator<int> pressValidator = new FloorPadPressValidator(floorPadBehaviour, logger, gameObjectName);
            IValidator<int> releaseValidator = new FloorPadReleaseValidator(floorPadBehaviour, logger, gameObjectName);

            return FloorPadBuilder.WithBehaviour(floorPadBehaviour).WithLogger(logger)
                                  .WithGameObjectName(gameObjectName).WithSpriteHandler(spriteHandler)
                                  .WithPressValidator(pressValidator).WithReleaseValidator(releaseValidator)
                                  .WithPressedChannel(pressedChannel).WithReleasedChannel(releasedChannel).Build();
        }
    }
}