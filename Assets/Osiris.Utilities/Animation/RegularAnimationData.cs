using ILogger = Osiris.Utilities.Logging.ILogger;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    public abstract class RegularAnimationData : DescriptionSO
    {
        [SerializeField] private float _MinimumInterval;
        [SerializeField] private float _MaximumInterval;

        public float MinimumInterval { get => _MinimumInterval; }
        public float MaximumInterval { get => _MaximumInterval; }

        public abstract string GetParameter();

        public abstract bool IsValid(Animator animator, string gameObjectName, ILogger logger);
    }
}
