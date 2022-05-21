using ILogger = Osiris.Utilities.Logging.ILogger;
using System.Linq;
using UnityEngine;
using Osiris.Utilities.Logging;

namespace Osiris.Utilities.Animation
{

    [CreateAssetMenu(fileName = AssetMenu.RegularTriggerAnimationDataFileName, menuName = AssetMenu.RegularTriggerAnimationDataPath)]
    public class SingleStateRegularAimationData : RegularAnimationData
    {
        [SerializeField] private string _Parameter;

        public string Parameter { get => _Parameter; }

        public override string GetParameter()
        {
            return _Parameter;
        }

        public override bool IsValid(Animator animator, string gameObjectName, ILogger logger)
        {
            if (!animator.parameters.Any(acp => acp.name == _Parameter))
            {
                logger.Log($"Animator contains no parameters with name {_Parameter}.",
                               gameObjectName, LogLevel.Error);
                return false;
            }

            return true;
        }
    }
}
