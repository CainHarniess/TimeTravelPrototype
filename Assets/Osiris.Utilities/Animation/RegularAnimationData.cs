using Osiris.Utilities.ScriptableObjects;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    [CreateAssetMenu(fileName = AssetMenu.RegularTriggerAnimationDataFileName, menuName = AssetMenu.RegularTriggerAnimationDataPath)]
    public class RegularAnimationData : DescriptionSO
    {
        [SerializeField] private string _ParameterName;
        [SerializeField] private float _MinFlickerDelay;
        [SerializeField] private float _MaxFlickerDelay;

        public string ParameterName { get => _ParameterName; }
        public float MinFlickerDelay { get => _MinFlickerDelay; }
        public float MaxFlickerDelay { get => _MaxFlickerDelay; }
    }
}
