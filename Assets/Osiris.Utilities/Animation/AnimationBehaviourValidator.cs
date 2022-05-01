using Osiris.Utilities.Logging;
using Osiris.Utilities.Validation;
using System.Linq;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.Utilities.Animation
{
    public class AnimationBehaviourValidator : IValidator
    {
        private readonly string _parameterName;
        private readonly Animator _animator;
        private readonly string _gameObjectName;
        private readonly ILogger _Logger;

        public AnimationBehaviourValidator(string parameterName, Animator animator, string gameObjectName,
                                           ILogger logger)
        {
            _parameterName = parameterName;
            _animator = animator;
            _gameObjectName = gameObjectName;
            _Logger = logger;
        }

        public bool IsValid()
        {
            if (!_animator.parameters.Any(acp => acp.name == _parameterName))
            {
                _Logger.Log($"Animator contains no parameters with name {_parameterName}.",
                               _gameObjectName, LogLevel.Error);
                return false;
            }

            return true;
        }
    }
}
