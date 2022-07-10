using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using System.Linq;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    [CreateAssetMenu(fileName = AssetMenu.MultiStateRegularTriggerAnimationDataFileName, menuName = AssetMenu.MultiStateRegularTriggerAnimationDataPath)]
    public class MultiStateRegularAnimationData : RegularAnimationData
    {
        [SerializeField] private string[] _Paramenters;
        
        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private int _PreviousIndex = 0;

        public string[] Parameters { get => _Paramenters; }


        public override string GetParameter()
        {
            string output = _Paramenters[_PreviousIndex];
            _PreviousIndex = (_PreviousIndex + 1) % _Paramenters.Length;
            return output;
        }

        public override bool IsValid(Animator animator, string gameObjectName, Logging.ILogger logger)
        {
            foreach(string parameter in _Paramenters)
            {
                if (animator.parameters.Any(acp => acp.name == parameter))
                {
                    continue;
                }
                logger.Log($"Animator contains no parameters with name {parameter}.", gameObjectName, LogLevel.Error);
                return false;
            }
            
            return true;
        }
    }
}
